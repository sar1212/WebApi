using System.ComponentModel.DataAnnotations;
using WebApi;

namespace Repository
{
    public interface IUsersRepository
    {
       
        Task<User> AddUser(User user);
        Task<User> GetUser(LoginUser loginUser);
        Task<User> GetUserByID(int id);
        Task<User> UpdateUser(User userToUpdate);
    }
}