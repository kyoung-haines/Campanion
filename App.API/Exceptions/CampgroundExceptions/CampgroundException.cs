namespace App.API.Exceptions.CampgroundExceptions
{
    public class CampgroundException : Exception
    {
        public CampgroundException() { }
        public CampgroundException(string message) : base(message) { }
        public CampgroundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
