
using System.Text.Json;
using Zxcvbn;

namespace Services
{
    public class PasswordStrength : IPasswordValidation
    {
        public Result GetPasswordStrength(string password)
        {
            return Zxcvbn.Core.EvaluatePassword(password);
        }

    }
}
