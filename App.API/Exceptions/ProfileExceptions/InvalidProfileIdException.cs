namespace App.API.Exceptions.ProfileExceptions
{
    public class InvalidProfileIdException : Exception
    {
        public InvalidProfileIdException(string message) : base(message)
        {
        }
        public InvalidProfileIdException()
        {
        }
        
        public InvalidProfileIdException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
