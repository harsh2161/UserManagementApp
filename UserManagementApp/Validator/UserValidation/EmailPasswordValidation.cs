using UserManagementApp.Models;

namespace UserManagementApp.Validator.UserValidation
{
    public static class EmailPasswordValidation
    {
        public static bool IsEmailPasswordValid(UserCredentials userCredentials)
        {
            return (EmailValidation.IsEmailValid(userCredentials.UserEmail) && PasswordValidation.IsPasswordValid(userCredentials.UserPassword));
        }
    }
}