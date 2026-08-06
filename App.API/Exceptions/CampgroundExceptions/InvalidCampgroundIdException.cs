namespace App.API.Exceptions.CampgroundExceptions
{
    public class InvalidCampgroundIdException : CampgroundException
    {
        public InvalidCampgroundIdException() { }
        public InvalidCampgroundIdException(string message = "Campground not found. Invalid CampgroundId value.") : base(message) { }
        public InvalidCampgroundIdException(Exception innerException, string message = "Campground not found. Invalid CampgroundId value.") : base(message, innerException) { } 
    }
}
