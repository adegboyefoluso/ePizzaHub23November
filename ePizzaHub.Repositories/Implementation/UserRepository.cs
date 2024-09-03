using ePizzaHub.Core;
using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ePizzaHub.Repositories.Implementation
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(AppDBContext db) :base(db) 
        {
                
        }
        public bool Create(User user, string Role)
        {
            try
            {
                Role role = _db.Roles.Where(r => r.Name == Role).FirstOrDefault();
                var existinguser= _db.Users.Where(u=>u.Email==user.Email).FirstOrDefault();
                if (existinguser==null)
                {
                    if (role != null)
                    {
                        user.Roles.Add(role);
                        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); //hashingpassword
                        _db.Users.Add(user);
                        _db.SaveChanges();
                        return true;
                    }
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return false;
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            User user=_db.Users.Include(r=>r.Roles).Where(u=> u.Email == Email ).FirstOrDefault();
            if(user != null)
            {
                bool isverify= BCrypt.Net.BCrypt.Verify(Password, user.Password);
               
                if(isverify)
                {
                    UserModel model = new UserModel
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = user.Roles.Select(r => r.Name).ToArray(),
                    };
                    return model;
                }
            }
            return null;
        }
    }
}
