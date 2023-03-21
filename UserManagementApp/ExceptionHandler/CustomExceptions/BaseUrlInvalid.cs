using System;

namespace UserManagementApp.ExceptionHandler.CustomExceptions
{
    public class BaseUrlInvalid : Exception
    {
        public BaseUrlInvalid() : base() { }
        public BaseUrlInvalid(string message, Exceptions getExceptions) : base(message) { }
        public BaseUrlInvalid(string message) : base(message) { }
    }
}