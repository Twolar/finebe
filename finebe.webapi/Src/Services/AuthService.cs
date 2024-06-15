using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using finebe.webapi.Src.Helpers;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Generic;
using finebe.webapi.Src.Models.Login;
using finebe.webapi.Src.Models.ResetPassword;
using finebe.webapi.Src.Models.Settings;
using finebe.webapi.Src.Persistence;
using finebe.webapi.Src.Persistence.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace finebe.webapi.Src.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOptions<AuthSettings> _authSettings;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            IOptions<AuthSettings> authSettings,
            IEmailService emailService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _authSettings = authSettings;
            _emailService = emailService;
            _context = context;
        }

        public async Task<AuthToken> LoginAsync(LoginRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return null;
            }

            var authClaims = await GenerateClaimsAsync(user);
            var authToken = GenerateToken(authClaims, user);
            var refreshToken = await GenerateRefreshToken(user);

            authToken.RefreshToken = refreshToken.Token;
            return authToken;
        }

        public async Task<AuthToken> RefreshTokenAsync(string token, string refreshToken)
        {
            var principal = GetPrincipalFromExpiredToken(token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(rt => 
                rt.Token == refreshToken && 
                string.Equals(rt.UserId.ToString(), userId, StringComparison.OrdinalIgnoreCase) &&
                !rt.IsRevoked);

            if (storedRefreshToken == null || storedRefreshToken.Expiration < DateTime.Now)
                return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var newAuthClaims = await GenerateClaimsAsync(user);

            // Invalidate the used refresh token
            storedRefreshToken.IsRevoked = true;
            _context.RefreshTokens.Update(storedRefreshToken);
            await _context.SaveChangesAsync();

            var newAuthToken = GenerateToken(newAuthClaims, user);
            var newRefreshToken = await GenerateRefreshToken(user);

            newAuthToken.RefreshToken = newRefreshToken.Token;
            return newAuthToken;
        }

        private async Task<List<Claim>> GenerateClaimsAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private AuthToken GenerateToken(IEnumerable<Claim> authClaims, ApplicationUser user)
        {
            var claimsIdentity = new ClaimsIdentity(authClaims, "Bearer");

            var secretKey = EnvVariableHelper.GetByKey("JWT_SECRET");
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new AuthToken
            {
                Token = tokenString,
                Expiration = token.ValidTo,
                IssuedAt = token.ValidFrom,
                UserId = user.Id,
                UserName = user.UserName,
                Roles = authClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToList(),
                Claims = authClaims.ToDictionary(c => c.Type, c => c.Value)
            };
        }

        private async Task<RefreshToken> GenerateRefreshToken(ApplicationUser user)
        {
            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                UserId = user.Id,
                Expiration = DateTime.Now.AddDays(7),
                IsRevoked = false
            };

            await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvVariableHelper.GetByKey("JWT_SECRET"))),
                ValidateLifetime = false // we want to get claims from expired tokens
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            if (securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return principal;
            }
            throw new SecurityTokenException("Invalid token");
        }
        
        public async Task<bool> RequestPasswordResetAsync(ResetPasswordActionRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return false;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"{_authSettings.Value.ResetPasswordUrl}/{token}";
            var message = $"To reset your password, please click on this link {resetLink}";

            await _emailService.SendEmailAsync(model.Email, "Reset Password", message);

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
            if (!resetPassResult.Succeeded)
                return false;

            return true;
        }

        public async Task<bool> ValidateResetTokenAsync(ValidateResetTokenRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return false;

            var validToken = await _userManager.VerifyUserTokenAsync(user,
                                _userManager.Options.Tokens.PasswordResetTokenProvider,
                                "ResetPassword", model.Token);

            return validToken;
        }
    }
}
