namespace App.API.Exceptions.CampgroundExceptions
{
    public class InvalidCampgroundId : CampgroundException
    {
        public InvalidCampgroundId() { }
        public InvalidCampgroundId(string message) : base(message) { }
        public InvalidCampgroundId(string message, Exception innerException) : base(message, innerException) { } 
    }
}
