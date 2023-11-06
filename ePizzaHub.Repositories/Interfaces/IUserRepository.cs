﻿using ePizzaHub.Core.Entities;
using ePizzaHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
       UserModel ValidateUser(string Email, string Password);

        bool Create(User user, string Role);

    }
}
