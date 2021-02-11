using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ThriftBooks.Business.Models.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ThriftBooks.Business.Services.Interfaces;

namespace ThriftBooks.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration,
            IUserService userRepository)
        {
            _configuration = configuration;
            _userService = userRepository;
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }

        public static bool IsPasswordMatching(string password, string passwordHash)
        {
            /* Extract the bytes */
            byte[] hashBytes = Convert.FromBase64String(passwordHash);
            /* Get the salt */
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            /* Compute the hash on the password the user entered */
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            /* Compare the results */
            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }

        public Task<string> Authenticate(LoginModel model)
        {
            var user = _userService.GetUserByUsername(model.Username);

            if (user == null || !IsPasswordMatching(model.Password, user.Password))
            {
                return Task.FromResult<string>(null);
            }

            return GenerateToken(user);
        }

        public Task<string> GenerateToken(UserAuthModel user)
        {
            var claims = PopulateTokenClaims(user);

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Issuer"],
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Task.FromResult(tokenString);
        }

        private List<Claim> PopulateTokenClaims(UserAuthModel user)
        {
            return new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
        }
    }
}
