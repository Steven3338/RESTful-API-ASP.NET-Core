using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CustomerDataWebApplicationExample.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeopleInformation.Data;
using PeopleInformation.Domain;

namespace CustomerDataWebApplicationExample.Controllers
{
    public class AuthController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private DisconnectedData _context;
        private IJwtGenerator _jwtGenerator { get; set; }

        public AuthController(
            UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            DisconnectedData context,
            IJwtGenerator jwtGenerator
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
            _jwtGenerator = jwtGenerator;
        }

        // register
        [Route("api/register")]
        [HttpPost]
        public IActionResult RegisterUser([FromBody] User model)
        {
            var user = _context.HandleUserRegistration(model);
            if (user == null) return Unauthorized();
   
            return Ok(_jwtGenerator.GenerateJwt(user));
            //return output ?? Unauthorized();
        }

        // login
        [Route("api/login")]
        [HttpPost]
        public ActionResult Login([FromBody] User model)
        {
            var user = _context.HandleLogin(model);
            if (user != null)
            {
                return Ok(_jwtGenerator.GenerateJwt(user));
            }
            return Unauthorized();
        }
    }
}