using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApi;
using Zxcvbn;
using System.Linq;

namespace Repository
{
    public class UsersRepository : IUsersRepository
    {
        ShopContext _shopContext;
        public UsersRepository(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        public async Task<User> GetUserByID(int id)
        {
            return await _shopContext.Users.FindAsync(id);
        }
        public async Task<User> GetUser(LoginUser loginUser)
        {
            User user = _shopContext.Users.ToList().Find(u => u.Email == loginUser.Email && u.Password == loginUser.Password);
            return user;
        }
        public async Task<User> AddUser(User user)
        {
            _shopContext.Users.AddAsync(user);
            await _shopContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUser(User userToUpdate)
        {
            var userToUpdateInDb = await _shopContext.Users.FindAsync(userToUpdate.Id);
            if (userToUpdateInDb == null)
            {
                return null;
            }
            _shopContext.Entry(userToUpdateInDb).CurrentValues.SetValues(userToUpdate);
            await _shopContext.SaveChangesAsync();
            return userToUpdate;
        }

        public async Task<int> RemoveUser(int id)
        {
            var user = await _shopContext.Users.FindAsync(id);
            if (user != null)
            {
                _shopContext.Users.Remove(user);
                await _shopContext.SaveChangesAsync();
                return id;
            }
            return -1;
        }

    }
}
