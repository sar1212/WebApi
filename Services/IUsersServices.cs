using WebApi;

namespace Services
{
    public interface IUsersServices
    {
        Task<User> GetUserByID(int id);
        Task<User> Login(LoginUser loginUser);
        //Task<int> RemoveUser(User userToRemove);
        Task<User> SignUp(User user);
        Task<User> UpdateUser(User userToUpdate);
    }
}