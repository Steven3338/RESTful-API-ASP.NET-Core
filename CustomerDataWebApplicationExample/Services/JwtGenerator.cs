using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeopleInformation.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDataWebApplicationExample.Services
{
    public class JwtGenerator: IJwtGenerator
    {
        private IConfiguration _configuration { get; set; }
        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwt(User user)
        {
            string securityKey = _configuration["Jwt:SigningKey"];
            //string securityKey = "example_security_key_for_token_validation_5_3_2019";

            // need symmetric security key
            var symmetricSecuityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            // create credentials for signing token
            // 1st parameter, our symmetric security key
            // 2nd parameter, algorithm that we will use to generate to sign token and to validate it
            var signingCredentials = new SigningCredentials(symmetricSecuityKey, SecurityAlgorithms.HmacSha256);

            // add claims
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, user.IsAdmin ? "Administrator" : "Customer"));
            claims.Add(new Claim("isAdmin", user.IsAdmin.ToString()));
            claims.Add(new Claim("Email", user.Email));
            claims.Add(new Claim("id", user.Id.ToString()));


            // create token
            // pass named parameters, named attributes in order to create token, those parameters will be used as initial
            // value to create and later on when we set up a JWT authorization scheme to validate the token sent by
            // client
            var token = new JwtSecurityToken(
                //issuer: "smesk.in",  // issuer can be any string
                //audience: "readers", // can also be any string
                expires: DateTime.Now.AddHours(1), // date when the token expires
                signingCredentials: signingCredentials, // assign signing credentials
                claims: claims
                );

            // return token
            // need JWT security handler to return from this token to return a string that can be used in requests to our
            // API
            // this method will return string version of this token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
