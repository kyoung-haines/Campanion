namespace App.API.Exceptions.AppUserExceptions
{
    public class AppUserException : Exception
    {
        public AppUserException() { }
        public AppUserException(string message) : base(message) { }
        public AppUserException(string message, Exception innerException) : base(message, innerException) { }
    }
}
