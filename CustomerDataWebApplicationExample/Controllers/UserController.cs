using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomerDataWebApplicationExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeopleInformation.Data;
using PeopleInformation.Domain;
using PeopleInformation.Domain.AntiCorruption.Domain;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerDataWebApplicationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private DisconnectedData _repo;
        private IJwtGenerator _jwtGenerator;

        public UserController(DisconnectedData repo, IJwtGenerator jwtGenerator)
        {
            _repo = repo;
            _jwtGenerator = jwtGenerator;
        }

        // GET api/values/5
        // allows a user to get his personal information, less his address
        [HttpGet]
        public ActionResult<UserProfile> Get()
        {
            var userClaims = GetJwtClaims();
            UserProfile user = userClaims != null ? _repo.LoadUserProfile(int.Parse(userClaims["id"])) : null;
            if (user != null) return Ok(user);
            return Unauthorized();
        }

        // PUT api/values/5
        // allows a user to update his personal information, less his address
        [HttpPut]
        public ActionResult Put([FromBody] UserProfile data)
        {
            var userClaims = GetJwtClaims();
            if (userClaims != null)
            {
                var mappedData = _repo.HandleUserProfile(userClaims, data);
                if (mappedData != null)
                {
                    var jwt = _jwtGenerator.GenerateJwt(mappedData);
                    return Ok(jwt);
                }
                // false represents no changes
                return Ok(false);
            }
            return Unauthorized();
        }

        private Dictionary<string, string> GetJwtClaims()
        {
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            if (principal != null)
            {
                Dictionary<string, string> userClaims = new Dictionary<string, string>();
                foreach (var claim in principal.Claims)
                {
                    userClaims.Add(claim.Type, claim.Value);
                }
                return userClaims;
            }
            return null;
        }
    }
}
