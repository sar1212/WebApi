using Zxcvbn;

namespace Services
{
    public interface IPasswordValidation
    {
        Result GetPasswordStrength(string password);
    }
}