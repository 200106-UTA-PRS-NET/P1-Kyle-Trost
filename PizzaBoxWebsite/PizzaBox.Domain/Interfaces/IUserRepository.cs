using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaBox.Domain.Interfaces
{
    public interface IUserRepository<T>
    {
        IEnumerable<T> GetUsers(string user = null, string pass = null);

        T GetUserById(string userName, string pass);
        void AddUser(T user);
        void ModifyUser(T user);
        void RemoveUser(int id);
    }
}
