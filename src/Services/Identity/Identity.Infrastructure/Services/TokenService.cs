using Identity.Core.Interfaces;
using Identity.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<TokenModel> GenerateToken(User user, UserManager<User> userManager)
        {
            var currentUser = await userManager.FindByIdAsync(user.Id);

            var userClaims = await userManager.GetClaimsAsync(currentUser);
            var claims = CreateClaims(userClaims, user);

            var tokenDescriptor = CreateSecurityTokenDescriptor(claims);

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenModel(tokenHandler.WriteToken(token), user.Id);
        }

        private List<Claim> CreateClaims(IList<Claim> userClaims, User user)
        {
            var roles = userClaims.Where(c => c.Type == ClaimTypes.Role).ToList();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            if (roles.Count() > 0 && roles != null)
                foreach (var role in roles)
                    claims.Add(new Claim(ClaimTypes.Role, role.Value));

            return claims;
        }

        private SecurityTokenDescriptor CreateSecurityTokenDescriptor(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            return new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
        }
    }
}
