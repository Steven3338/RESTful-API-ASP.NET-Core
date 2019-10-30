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
    public class UserAddressesController : ControllerBase
    {

        private DisconnectedData _repo;
        //private IJwtGenerator _jwtGenerator;

        public UserAddressesController(DisconnectedData repo, IJwtGenerator jwtGenerator)
        {
            _repo = repo;
           //_jwtGenerator = jwtGenerator;
        }

        [HttpGet]
        public ActionResult<Address> Get()
        {
            var userClaims = GetJwtClaims();
            List<Address> addresses = userClaims != null ? _repo.LoadUserAddresses(int.Parse(userClaims["id"])) : null;
            if (addresses != null) return Ok(addresses);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Object> Get(int id)
        {
            var userClaims = GetJwtClaims();
            Address address = userClaims != null ? _repo.LoadAddress(int.Parse(userClaims["id"]), id) : null;
            if (address != null)
            {
                var addressParametersFiltered = new { address.Street, address.City, address.County, address.Zip, address.State, address.Country, address.MoveInDate, address.MoveOutDate, address.Id, address.UserId };
                return Ok(addressParametersFiltered);
            }
            return Ok();
        }

        // Add a new address to the Database
        [HttpPost]
        public ActionResult Post([FromBody] Address address)
        {
            var userClaims = GetJwtClaims();
            if (userClaims != null)
            {
                bool output = false;
                if(address != null) output = _repo.HandleAddressSubmission(userClaims, address);
                return Ok(output ? "Address added to the database" : "No changes made to database");
            }
            return Unauthorized();
        }

        // Revise an existing address
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Address address)
        {
            var userClaims = GetJwtClaims();
            if (userClaims != null)
            {
                bool output = false;
                if (address != null) output = _repo.HandleAddressUpdate(userClaims, address);
                return Ok(output ? "Address updated in the database" : "No changes made to database");
            }
            return Unauthorized();
        }

        // allows a user to delete an address
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var userClaims = GetJwtClaims();
            var deleted = _repo.DeleteAddress(int.Parse(userClaims["id"]), id);
            if (deleted == "unauthorized") return Unauthorized();
            return Ok(bool.Parse(deleted));
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