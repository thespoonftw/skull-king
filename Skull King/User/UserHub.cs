using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User
{
    public class UserHub : Hub
    {
        private IUserService userService;

        public UserHub(IUserService userService)
        {
            this.userService = userService;
        }

        public void AddUser(string user)
        {
            if (userService.GetUsers().Contains(user)) 
            {
                Clients.Caller.SendAsync("User", false);
            } 
            else
            {
                userService.AddUser(user);
                Clients.Caller.SendAsync("User", true);
                SendUsersUpdate();
            }            
        }

        public void SendUsersUpdate()
        {
            Clients.All.SendAsync("UsersUpdate", userService.GetUsers());
        }
    }
}
