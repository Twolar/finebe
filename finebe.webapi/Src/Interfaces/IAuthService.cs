using finebe.webapi.Src.Models.Generic;
using finebe.webapi.Src.Models.Login;
using finebe.webapi.Src.Models.ResetPassword;

namespace finebe.webapi.Src.Interfaces;

public interface IAuthService
{
    Task<AuthToken> LoginAsync(LoginRequestDto model);
    Task<AuthToken> RefreshTokenAsync(string token, string refreshToken);
    Task<bool> RequestPasswordResetAsync(ResetPasswordActionRequestDto model);
    Task<bool> ResetPasswordAsync(ResetPasswordRequestDto model);
    Task<bool> ValidateResetTokenAsync(ValidateResetTokenRequestDto model);
}