using HM.Data.DataTransferObjects;
using HM.DataAccess.Contracts;
using HM.Services.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static HM.Services.Contracts.IUserService;

namespace HM.Services.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtBearerOptions _jwtBearerTokenOptions;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string stringKey;
        #endregion

        #region Constructor
        public UserService(IOptions<JwtBearerOptions> jwtBearerTokenOptions,
                           UserManager<IdentityUser> userManager,
                           IUnitOfWork unitOfWork,
                           IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _jwtBearerTokenOptions = jwtBearerTokenOptions.Value;
            _userManager = userManager;
            stringKey = configuration["Key"];

        }
        #endregion


        public async Task<IdentityResult> CreateUserAsync(UserForCreateDto userDto)
        {

            var identityUser = new IdentityUser() { UserName = userDto.UserName, Email = userDto.Email };
            return await _userManager.CreateAsync(identityUser, userDto.Password); ;
        }

        public async Task<LoginStatus> AttemptLoginAsync(UserForLoginDto userDto)
        {

            var identityUser = await _userManager.FindByNameAsync(userDto.Username);
            if (identityUser == null)
                return LoginStatus.WrongEmail;

            var passwordResult =  _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, userDto.Password);

            if (passwordResult != PasswordVerificationResult.Success) 
            {
                if (_userManager.SupportsUserLockout && await _userManager.GetLockoutEnabledAsync(identityUser))
                    await _userManager.AccessFailedAsync(identityUser);

                return LoginStatus.WrongPassword;
            }
            else
            {
                if (identityUser.LockoutEnd.HasValue && identityUser.LockoutEnd > DateTime.UtcNow)
                    return LoginStatus.Lockout;

                if (_userManager.SupportsUserLockout && await _userManager.GetAccessFailedCountAsync(identityUser) > 0)
                    await _userManager.ResetAccessFailedCountAsync(identityUser);
            }

            return LoginStatus.Success;

        }

        public async Task<bool> IsUserValidAsync(string userId, string token)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            return identityUser != null && IsJwtTokenValid(userId, token);
        }

        public async Task<string> GetJwtTokenAsync(UserForLoginDto userDto)
        {
            var identityUser = await _userManager.FindByNameAsync(userDto.Username);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(stringKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("userId", identityUser.Id.ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtBearerTokenOptions.Audience,
                Issuer = _jwtBearerTokenOptions.ClaimsIssuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);



        }


        #region Helpers
        private bool IsJwtTokenValid(string userId, string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(stringKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var readUserId = jwtToken.Claims.First(x => x.Type == "userId")?.Value;

                return readUserId == userId;
            }
            catch
            {
                return false;
            }
        }
        #endregion

    }
}
