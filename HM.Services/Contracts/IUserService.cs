using HM.Data.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HM.Services.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> CreateUserAsync(UserForCreateDto userDto);

        Task<LoginStatus> AttemptLoginAsync(UserForLoginDto userDto);
        Task<bool> IsUserValidAsync(string userId, string token);
        Task<string> GetJwtTokenAsync(UserForLoginDto userDto);

        #region Enums
        public enum LoginStatus
        {
            Success,
            WrongEmail,
            WrongPassword,
            Lockout
        }
        #endregion
    }
}
