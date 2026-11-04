using System.Text.Json;
using WebApi;
using Repository;

namespace Services
{
    public class UsersServices
    {
        UsersRepository usersRepository = new UsersRepository();
        public User GetUserByID(int id)
        {
          return usersRepository.GetUserByID(id);
        }
        public User Login(LoginUser loginUser)
        {
           return usersRepository.Login(loginUser);
        }
        public User SignUp(User user)
        {
          return usersRepository.SignUp(user);
        }
        public bool Update(User userToUpdate)
        {
            return usersRepository.Update(userToUpdate);
        }
        public int RemoveUser(User userToRemove)
        {
            return usersRepository.RemoveUser(userToRemove.UserId);
        }
    }
}
