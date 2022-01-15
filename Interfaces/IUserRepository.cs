using MALON_GLOBAL_WEBAPP.Models;
using System.Collections;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Interfaces
{
    public interface IUserRepository
    {
        bool CreateNewAccount(UserAccount newUserAccount);
        public IEnumerable GetUser();
        public object GetUserById(int userId);
        public bool DeleteUser(int userId);
        public void UpdateFirstAndLastName(UserAccount newUser, int userId);
        Task<UserAccount> ValidLogin(string emailAddress, string password);

    }
}
