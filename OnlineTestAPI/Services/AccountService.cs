using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineTestAPI.Entities;
using OnlineTestAPI.Models;
using OnlineTestAPI.DBAccess;
using System.Linq;

namespace OnlineTestAPI.Services
{
    
     
public class AccountService: IAccountService
    {
        private OnlineTestDBContext dBContext;

        public object DBContext { get; private set; }

        public AccountService(OnlineTestDBContext dBContext)
    {
        this.dBContext = dBContext;
    }

    public void Register(User user)
    {
        dBContext.Users.Add(user);
        dBContext.SaveChanges();
    }

    public User ValidateUser(Login login)
    {
        User user = dBContext.Users.SingleOrDefault(u => u.Email == login.Email && u.Password == login.Password);
        return user;
    }
      public  List <User> GetAllUsers()
        {
            return dBContext.Users.ToList();
        }

}
}
