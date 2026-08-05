namespace App.API.Exceptions.AppUserFavouriteCampgroundExceptions
{
    public class InvalidAppUserFavouriteCampgroundReference : NullReferenceException
    {
        public InvalidAppUserFavouriteCampgroundReference() { }

        public InvalidAppUserFavouriteCampgroundReference(string message  = "Invalid favourite campground object. The reference is null.") : base(message) { }

        public InvalidAppUserFavouriteCampgroundReference(string message = "Invalid favourite campground object. The reference is null.", Exception innerException) : base(message, innerException) { }
    }
}
