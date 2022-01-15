using MALON_GLOBAL_WEBAPP.Models;
using System.Collections;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Interfaces
{
    public interface IUserService
    {
        bool CreateNewAccountAsync(UserAccount newUserAccount);
        bool DeleteUserAsync(int userId);
        IEnumerable GetUserAsync();
        object GetUserByIdAsync(int userId);
        void UpdateFirstAndLastNameAsync(UserAccount newUser, int userId);
        Task<UserAccount> ValidLoginAsync(string emailAddress, string password);
    }
}
