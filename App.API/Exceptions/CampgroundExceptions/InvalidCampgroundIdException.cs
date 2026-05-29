namespace App.API.Exceptions.CampgroundExceptions
{
    public class InvalidCampgroundIdException : CampgroundException
    {
        public InvalidCampgroundIdException() { }
        public InvalidCampgroundIdException(string message) : base(message) { }
        public InvalidCampgroundIdException(string message, Exception innerException) : base(message, innerException) { } 
    }
}
