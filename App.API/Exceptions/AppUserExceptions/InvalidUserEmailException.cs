namespace App.API.Exceptions.AppUserExceptions
{
    public class InvalidUserEmailException : Exception
    {
        public InvalidUserEmailException() { }
        public InvalidUserEmailException(string message) : base(message) { }
        public InvalidUserEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}
