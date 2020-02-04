using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PizzaBox.Domain;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories
{
    public class UserRepository : IUserRepository<User>
    {
        PizzaBoxDbContext db;

        public UserRepository()
        {
            db = new PizzaBoxDbContext();
        }

        public UserRepository(PizzaBoxDbContext db)
        {
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddUser(User user)
        {
            //throw new NotImplementedException();

            if (db.Users.Any(u => u.UserName == user.UserName) || user.UserName == null)
            {
                Console.WriteLine($"User {user.UserName} already exists and cannot be added");
                return;
            }
            else
            {
                Console.WriteLine($"Adding user {user.UserName}...");
                db.Users.Add(Mapper.MapUser(user));
                db.SaveChanges();
                Console.WriteLine($"User {user.UserName} added successfully");
            }
        }

        public User GetUserById(string userName, string pass)
        {
            var user = from u in db.Users.Where(u => u.UserName == userName && u.UserPass == pass)
                       select u;

            if (user != null)
                return Mapper.MapUser(user.FirstOrDefault());
            else
                return null;
        }

        public IEnumerable<User> GetUsers(string user = null, string pass = null)
        {
            var query = (user != null && pass != null)
                        ? from u in db.Users
                          where u.UserName == user && u.UserPass == pass
                          select Mapper.MapUser(u)
                        : from u in db.Users
                          //where u.UserName == user && u.UserPass == pass
                          select Mapper.MapUser(u);

            return query;
        }

        public void ModifyUser(User user)
        {
            if (db.Users.Any(u => u.UserId == user.UserId))
            {
                var userTemp = db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                userTemp.UserName = user.UserName;
                userTemp.UserPass = user.UserPass;
                db.Users.Update(userTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"User with ID {user.UserId} does not exist");
                return;
            }
        }

        public void RemoveUser(int id)
        {
            var userTemp = db.Users.FirstOrDefault(u => u.UserId == id);
            if (userTemp.UserId == id)
            {
                db.Remove(userTemp);
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine($"User with ID {id} does not exist");
                return;
            }
        }
    }
}
