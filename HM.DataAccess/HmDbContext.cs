using HM.DataAccess.Base;
using HM.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace HM.DataAccess
{
    public class HmDbContext: IdentityDbContext<IdentityUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly byte[] _encryptionKey;
        private readonly byte[] _encryptionIV;
        private readonly IEncryptionProvider _encryptionProvider;

        #region Constructor
        public HmDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _encryptionKey = Encoding.ASCII.GetBytes(configuration["EncryptionKey"]);
            _encryptionIV = Encoding.ASCII.GetBytes(configuration["EncryptionIV"]);
            _encryptionProvider = new AesProvider(_encryptionKey, _encryptionIV);
        }
        #endregion


        #region DbSets 
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerEvent> CustomerEvents { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomEvent> RoomEvents { get; set; }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeShift> EmployeeShifts { get; set; }
        #endregion


        #region Public Methods
        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        #endregion

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_encryptionProvider);

            modelBuilder.Entity<Customer>().HasIndex(x => x.Email).IsUnique();

            base.OnModelCreating(modelBuilder);


        }

        #endregion

        #region Helpers
        private void UpdateTimestamps()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst("userId")?.Value;
            Guid? userId = string.IsNullOrWhiteSpace(currentUserId) ? null : new Guid(currentUserId);
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.UtcNow;
                    ((BaseEntity)entity.Entity).CreatorUserId = userId;
                }

                ((BaseEntity)entity.Entity).DateModified = DateTime.UtcNow;
                ((BaseEntity)entity.Entity).ModifierUserId = userId;
            }
        }
        #endregion
    }
}
