using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
    public class UserMessageController : ControllerBase
    {
        private DisconnectedData _repo;

        public UserMessageController(DisconnectedData repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public ActionResult<Object> Get(int id)
        {
            var userClaims = GetJwtClaims();
            if(userClaims != null)
            {
                var output = _repo.GetUserCaseAndMessages(id, int.Parse(userClaims["id"]));
                if (output != null) return Ok(output);
                return NotFound();
            }
            return Unauthorized();
        }

        [HttpPost("{id}")]
        public ActionResult Post(int id, [FromBody] Message message)
        {
            var userClaims = GetJwtClaims();
            if(userClaims != null || message == null || message.MessageText != "")
            {
                var output = _repo.HandleNewMessageSubmission(id, int.Parse(userClaims["id"]), message);
                return output != false ? Ok("Saved to the database") : Ok("No Changes to Database");
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