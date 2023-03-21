using System;
using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.UserValidation
{
    public static class DateTimeValidation
    {
        public static bool IsDateTimeValid(DateTime DateOfBirth)
        {
            if (DateOfBirth.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                throw new ValidationException("Date Time Improper Format");
            }
            DateOfBirth = DateOfBirth.AddDays(1);

            if (DateOfBirth > DateTime.Now)
            {
                throw new ValidationException("DateOfBirth Should be Less Than Today");
            }

            return true;
        }
    }
}