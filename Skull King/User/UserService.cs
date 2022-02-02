using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User
{
    public class UserService : IUserService
    {
        public event Action<List<string>> UsersUpdated;

        private List<string> joinedUsers = new List<string>();

        public void AddUser(string name)
        {
            joinedUsers.Add(name);
            //UsersUpdated?.Invoke(joinedUsers.ToList());
        }

        public void RemoveUser(string name)
        {
            joinedUsers.Remove(name);
            //UsersUpdated?.Invoke(joinedUsers.ToList());
        }

        public List<string> GetUsers()
        {
            return joinedUsers.ToList();
        }
    }
}
