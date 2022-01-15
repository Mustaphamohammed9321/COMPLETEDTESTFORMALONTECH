using MALON_GLOBAL_WEBAPP.Interfaces;
using MALON_GLOBAL_WEBAPP.Models;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace MALON_GLOBAL_WEBAPP.Services
{
    public class UserService : IUserService
    {
        protected readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CreateNewAccountAsync(UserAccount newUserAccount)
        {
            return _userRepository.CreateNewAccount(newUserAccount);

        }

        public IEnumerable GetUserAsync()
        {
            return _userRepository.GetUser();
        }

        public object GetUserByIdAsync(int userId)
        {

            return _userRepository.GetUserById(userId);
        }

        public bool DeleteUserAsync(int userId)
        {
            return _userRepository.DeleteUser(userId);
        }

        public void UpdateFirstAndLastNameAsync(UserAccount newUser, int userId)
        {
            _userRepository.UpdateFirstAndLastName(newUser, userId);
        }

        public async Task<UserAccount> ValidLoginAsync(string emailAddress, string password)
        {
            return await _userRepository.ValidLogin(emailAddress, password);
        }

    }
}
