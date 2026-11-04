using System.Text.Json;
using WebApi;
using Zxcvbn;

namespace Repository
{
    public class UsersRepository
    {
        const string filePath = "../WebApi/users.txt";
        public User GetUserByID(int id)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user =
                    JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.UserId == id)
                        return user;
                }
            }
            return null;
        }
        public User Login(LoginUser loginUser)
        {
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string? currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.Email == loginUser.Email && user.Password == loginUser.Password)
                        return user;
                }
            }
            return null;

        }
        public User SignUp(User user)
        {
            if (passwordValidation(user.Password) <= 2)
            {
                return null;
            }
            int maxId = 0;
            foreach (string line in System.IO.File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                var u = JsonSerializer.Deserialize<User>(line);
                if (u.UserId > maxId) maxId = u.UserId;
            }
            user.UserId = maxId + 1;
            string userJson = JsonSerializer.Serialize(user);
            System.IO.File.AppendAllText(filePath, userJson + Environment.NewLine);
            return user;
        }
        public bool Update(User userToUpdate)
        {
            string textToReplace = string.Empty;
            using (StreamReader reader = System.IO.File.OpenText(filePath))
            {
                string currentUserInFile;
                while ((currentUserInFile = reader.ReadLine()) != null)
                {
                    User user = JsonSerializer.Deserialize<User>(currentUserInFile);
                    if (user.UserId == userToUpdate.UserId)
                        textToReplace = currentUserInFile;
                }
            }
            if (textToReplace != string.Empty)
            {
                string text = System.IO.File.ReadAllText(filePath);
                text = text.Replace(textToReplace, JsonSerializer.Serialize(userToUpdate));
                System.IO.File.WriteAllText(filePath, text);
            }
            return true;
        }
        private static int passwordValidation(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password).Score;
        }
        public int RemoveUser(int id)
        {
           // Not now...
            return -1;
        }

    }
}
