using System;

namespace UserManagementApp.ExceptionHandler
{
    [Serializable]
    public class AuthorizationException : Exception
    {
        public AuthorizationException() : base() { }
        public AuthorizationException(string message, Exceptions getExceptions) : base(message) { }
        public AuthorizationException(string message) : base(message) { }
    }
}