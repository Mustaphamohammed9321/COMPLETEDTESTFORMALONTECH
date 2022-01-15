using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MALON_GLOBAL_WEBAPP.Helper
{
    public class JwtAuthenticationManager
    {
        protected string keyy;
        protected readonly IConfiguration _config;
        public JwtAuthenticationManager(IConfiguration config)
        {
            _config = config;
            //key = _config["JWTKey"];
            keyy = _config["Key"];
        }

        //private readonly IJwtAuthenticationManager _jwtManager;


        public string GenerateToken(string email)
        {
            byte[] nKey = Convert.FromBase64String(keyy.ToString()); 
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(nKey);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {new Claim(ClaimTypes.Email, email)}),
                Expires  = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if(jwtToken == null)
                    return null;
                byte[] nKey = Convert.FromBase64String(keyy);
                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(nKey),
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string ValidateToken(string token)  //this returns the details of the user
        {
            string email = null;
            ClaimsPrincipal claimsPrincipal = GetPrincipal(token);
            if (claimsPrincipal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)claimsPrincipal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim emailClaim = identity.FindFirst(ClaimTypes.Email);
            email = emailClaim.Value;
            return email;
        }










    }
}
