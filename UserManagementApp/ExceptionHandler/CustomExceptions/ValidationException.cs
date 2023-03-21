using System;

namespace UserManagementApp.ExceptionHandler
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException() : base() { }
        public ValidationException(string message, Exceptions getExceptions) : base(message) { }
        public ValidationException(string message) : base(message) { }
    }
}