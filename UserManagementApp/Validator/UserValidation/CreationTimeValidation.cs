using System;
using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.UserValidation
{
    public static class CreationTimeValidation
    {
        public static bool IsCreationTimeValid(DateTime DateOfBirth)
        {
            if (DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
            {
                throw new ValidationException("Creation Time Improper Format");
            }
            if (DateOfBirth > DateTime.Now)
            {
                throw new ValidationException("Creation Time Should be Less Equal To Today");
            }
            return true;
        }
    }
}