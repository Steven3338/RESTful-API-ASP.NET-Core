using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeopleInformation.Data;

namespace CustomerDataWebApplicationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {

        private DisconnectedData _repo;

        public AdminController(DisconnectedData repo)
        {
            _repo = repo;
        }

        // GET api/values
        // allows an admin to get a list of all users
        [HttpGet]
        //public IEnumerable<KeyValuePair<int, string>> Get()
        public List<Object> Get()
        {
            return _repo.GetCustomerList();
        }

        // GET api/values/5
        // allows an admin to get detailed information on any user
        [HttpGet("{id}")]
        public Object Get(int id)
        {
            return _repo.LoadUserGraph(id);
        }

        // DELETE api/values/5
        // allows an admin to delete any user
        [HttpDelete("{id}")]
        public OkObjectResult Delete(int id)
        {
            var deleted = _repo.DeleteCustomerGraph(id);
            return Ok(deleted);
        }
    }
}