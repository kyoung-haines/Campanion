namespace App.API.Exceptions.AppUserFavouriteCampgroundExceptions
{
    public class InvalidAppUserFavouriteCampgroundReferenceException : NullReferenceException
    {
        public InvalidAppUserFavouriteCampgroundReferenceException() { }

        public InvalidAppUserFavouriteCampgroundReferenceException(string message  = "Invalid favourite campground object. The reference is null.") : base(message) { }

        public InvalidAppUserFavouriteCampgroundReferenceException(Exception innerException, string message = "Invalid favourite campground object. The reference is null.") : base(message, innerException) { }
    }
}
