using System.Text.Json;
using WebApi;
using Repository;

namespace Services
{
    public class UsersServices : IUsersServices
    {
        PasswordStrength PasswordStrength = new PasswordStrength();
        private readonly IUsersRepository userRepository;
        public UsersServices(IUsersRepository userRepository1)
        {
            userRepository = userRepository1;
        }
        public async Task<User> GetUserByID(int id)
        {
            return await userRepository.GetUserByID(id);
        }
        public async Task<User> Login(LoginUser loginUser)
        {
            return await userRepository.GetUser(loginUser);
        }
        public async Task<User> SignUp(User user)
        {
            if (PasswordStrength.GetPasswordStrength(user.Password).Score <= 2)
            {
                // Password is too weak.
            }
            return await userRepository.AddUser(user);
        }
        public async Task<User> UpdateUser(User userToUpdate)
        {
            return await userRepository.UpdateUser(userToUpdate);
        }
        //public async Task<int> RemoveUser(User userToRemove)
        //{
        //    return await userRepository(userToRemove.Id);
        //}
    }
}
