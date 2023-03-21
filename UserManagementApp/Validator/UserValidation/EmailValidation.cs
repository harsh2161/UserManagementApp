﻿using System;
using System.Net.Mail;
using UserManagementApp.ExceptionHandler;

namespace UserManagementApp.Validator.UserValidation
{
    public static class EmailValidation
    {
        public static bool IsEmailValid(string email)
        {
            if (email == null || email == "")
            {
                throw new ValidationException("Null Email");
            }
            if (email.Length > 50)
            {
                throw new ValidationException("Email Size Limit 50");
            }
            try
            {
                MailAddress Email = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new ValidationException("Improper Email Format");
            }

            if (email.Contains("<") || email.Contains("/") || email.Contains(">") || email.Contains("\"") || email.Contains(";"))
            {
                throw new ValidationException("Improper Email Format");
            }
            return true;
        }
    }
}