namespace App.API.Exceptions.AppUserFavouriteCampgroundExceptions
{
    public class InvalidAppUserFavouriteCampgroundReferenceException : NullReferenceException
    {
        public InvalidAppUserFavouriteCampgroundReferenceException() { }

        public InvalidAppUserFavouriteCampgroundReferenceException(string message  = "Invalid favourite campground object. The reference is null.") : base(message) { }

        public InvalidAppUserFavouriteCampgroundReferenceException(string message = "Invalid favourite campground object. The reference is null.", Exception innerException) : base(message, innerException) { }
    }
}
