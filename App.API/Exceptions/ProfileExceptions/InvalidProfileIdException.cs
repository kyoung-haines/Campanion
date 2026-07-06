namespace App.API.Exceptions.ProfileExceptions
{
    public class InvalidProfileIdException : Exception
    {
        public InvalidProfileIdException(string message = "No profile found with the associated ProfileId.") : base(message)
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
