using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userTracker;

        public UserController(IUserService userTracker)
        {
            this.userTracker = userTracker;
        }

        [HttpPost]
        [Route("user/{name}")]
        public bool Post(string name)
        {
            userTracker.AddUser(name);
            return true;
        }
    }
}
