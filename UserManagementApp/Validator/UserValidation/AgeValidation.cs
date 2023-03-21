using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.UserValidation
{
    public static class AgeValidation
    {
        public static bool IsAgeValid(int age)
        {
            if (age <= 0 || age >= 120)
            {
                throw new ValidationException("Age Range 0 To 120");
            }
            return true;
        }
    }
}