using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication.Model.DB;

namespace WebApplication.Model.Repository
{
    public interface IUserRepository
    {

        public IEnumerable<User> GetSukaUsers();
        public void AddUser(User user);
    }

    public class UserRepository
    {
        // private UserDbContext userDbContext;
        //
        // public UserRepository(UserDbContext userDbContext)
        // {
        //     this.userDbContext = userDbContext;
        // }
        //
        // IEnumerable<User> IUserRepository.GetSukaUsers()
        // {
        //     return userDbContext.User.Include(x => x.Username);
        // }
        //
        // public void AddUser(User user) => userDbContext.User.Add(user);
    }    
}