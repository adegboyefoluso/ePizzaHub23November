using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class AuthService : Service<User>, IAuthService
    {
        IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository):base(userRepository)
        {
            _userRepository = userRepository;
        }
        public bool CreateUser(User user, string Role)
        {
            return _userRepository.Create(user, Role);
        }

        public UserModel ValidateUser(string Email, string Password)
        {
            return _userRepository.ValidateUser(Email, Password);
        }
    }
}
