using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CustomerDataWebApplicationExample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleInformation.Data;
using PeopleInformation.Domain;

namespace CustomerDataWebApplicationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserCaseController : ControllerBase
    {
        private DisconnectedData _repo;
        //private IJwtGenerator _jwtGenerator;

        public UserCaseController(DisconnectedData repo, IJwtGenerator jwtGenerator)
        {
            _repo = repo;
            //_jwtGenerator = jwtGenerator;
        }

        [HttpGet]
        public ActionResult<Case> Get()
        {
            var jwt = GetJwtClaims();
            if (jwt != null)
            {
                var cases = _repo.GetUserCases(int.Parse(jwt["id"]));
                return Ok(cases);
            }
            return Unauthorized();
        }

        [HttpPost]
        public ActionResult<Case> Post([FromBody] Case caseInput)
        {
            var jwt = GetJwtClaims();
            if (jwt != null)
            {
                var output = _repo.HandleCreateNewUserCase(int.Parse(jwt["id"]), caseInput);
                return output != null ? Ok(output) : Ok("Case was not saved to the database");
            }
            return Unauthorized();
        }

        // this controller changes the Resolved parameter to false
        [HttpPut("{id}")]
        public ActionResult Put(int id)
        {
            var userClaims = GetJwtClaims();
            if (userClaims != null)
            {
                var cases = _repo.HandleCloseUserCase(int.Parse(userClaims["id"]), id);
                if (cases != false) return Ok();
                return NotFound();
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