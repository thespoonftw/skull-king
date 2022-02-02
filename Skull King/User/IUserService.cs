using System;
using System.Collections.Generic;

namespace User
{
    public interface IUserService
    {
        void AddUser(string name);
        void RemoveUser(string name);
        List<string> GetUsers();

        event Action<List<string>> UsersUpdated;
    }
}
