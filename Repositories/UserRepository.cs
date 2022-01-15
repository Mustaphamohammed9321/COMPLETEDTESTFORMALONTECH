using MALON_GLOBAL_WEBAPP.Data;
using MALON_GLOBAL_WEBAPP.Helper;
using MALON_GLOBAL_WEBAPP.Interfaces;
using MALON_GLOBAL_WEBAPP.Models;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly IConfiguration _config;
        private readonly ProjDbContext _dbContext;
        protected readonly Crypt _crypt;
        public UserRepository(IConfiguration config, ProjDbContext dbContext, Crypt crypt)
        {
            _config = config;
            _dbContext = dbContext;
            _crypt = crypt; 
        }


        public bool CreateNewAccount(UserAccount newUserAccount)
        {
            using (_dbContext)
            {
                var pswd = _crypt.Encrypt(newUserAccount.Password).ToString();
                var oldUsers = _dbContext.UserAccounts.ToList();
                if (!oldUsers.Any(u => u.Email == newUserAccount.Email)) //check if a record exists from the list
                {
                    //add user here
                    var newUser = new UserAccount
                    {
                        FirstName = newUserAccount.FirstName,
                        LastName = newUserAccount.LastName,
                        Email = newUserAccount.Email,
                        Password = pswd,
                        DateCreated = System.DateTime.Now,

                    };
                    _dbContext.UserAccounts.Add(newUser);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IEnumerable GetUser()
        {
            try
            {
                using (_dbContext)
                {
                    var listofUsers = _dbContext.UserAccounts.ToList();
                    return listofUsers;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }

        public object GetUserById(int userId)
        {

            try
            {
                using (_dbContext)
                {
                    var user = _dbContext.UserAccounts.Where(e => e.UserId == userId).FirstOrDefault();
                    return user;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                using (_dbContext)
                {
                    var oldUsers = _dbContext.UserAccounts.ToList();
                    var userToDelete = oldUsers.Where(t => t.UserId == userId).FirstOrDefault();
                    if (userToDelete != null)
                    {
                        _dbContext.UserAccounts.Remove(userToDelete);
                        _dbContext.SaveChanges(true);
                        return true;
                    }
                    else { return false; }
                }
                
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void UpdateFirstAndLastName(UserAccount newUser, int userId)  
        {
            try
            {
                using (_dbContext)
                {
                    var oldUsers = _dbContext.UserAccounts.ToList();
                    var userToUpdate = oldUsers.Where(t => t.UserId == userId).FirstOrDefault();
                    if(userToUpdate != null)
                    {
                        userToUpdate.FirstName = newUser.FirstName;
                        userToUpdate.LastName = newUser.LastName;
                    }
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<UserAccount> ValidLogin(string emailAddress, string password)
        {
            try
            {
                using (_dbContext)
                {
                    var user = _dbContext.UserAccounts.Where(q => q.Email == emailAddress && q.Password == _crypt.Encrypt(password)).SingleOrDefault();
                    if (user != null)
                        return user;
                    return null;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}
