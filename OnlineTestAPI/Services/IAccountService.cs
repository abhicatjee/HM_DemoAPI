using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestAPI.Entities;
using OnlineTestAPI.Models;

namespace OnlineTestAPI.Services
{
    interface IAccountService
    {
        void Register(User user);
        User ValidateUser(Login login);
        List<User> GetAllUsers();
    }
}
