namespace App.API.Exceptions.AppUserExceptions
{
    public class InvalidUserIdException : AppUserException
    {
        public InvalidUserIdException() { }
        public InvalidUserIdException(string message) : base(message) { }
        public InvalidUserIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
