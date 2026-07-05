namespace App.API.Exceptions.TripExceptions
{
    public class TripNotFoundException : Exception
    {
        public TripNotFoundException() { }
        public TripNotFoundException(string message) : base(message) { }
        public TripNotFoundException(string message, Exception innerException) : base(message, innerException) { } 
    }
}
