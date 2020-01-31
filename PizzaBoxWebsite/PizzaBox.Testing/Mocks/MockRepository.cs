using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using System.Collections.Generic;

namespace PizzaBox.Testing.Mocks
{
    public class MockRepository : IUserRepository<User>
    {
        static IEnumerable<User> users = new List<User>()
        {
            new User()
            {
                UserId = 1,
                UserName = "Jonathan",
                UserPass = "qwerty"
            },
            new User()
            {
                UserId = 2,
                UserName = "Joseph",
                UserPass = "qwerty"
            }
        };

        public void AddUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetUserById(string userName, string pass)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<User> GetUsers(string user = null, string pass = null)
        {
            return users;
        }

        public void ModifyUser(User user)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveUser(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
