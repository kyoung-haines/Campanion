namespace App.API.Models.AppUser
{
    /// <summary>
    /// Class <c>Profile</c> represents the public-facing representation of a given user.
    /// <br />
    /// The <c>Profile</c> highlights various information about the user associated with the profile
    /// to the public. What is displayed can be selected by the user.
    /// <br />
    /// The <c>Profile</c> has two main purposes: to allow other uses to find and view some information about other users,
    /// as well as providing the ability to send friend requests to other users for the purpose of inviting them on Trips.
    /// </summary>
    public class Profile
    {
        /// <summary>
        ///  Property <c>ProfileId</c> represents the unique ID for a given Profile object.
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Property <c>ProfileImagePath</c> represents the local path to the user profile's profile image.
        /// <remarks>
        /// This will default to the placeholder profile image upon profile creation.
        /// <br />
        /// Users can upload an image to be used as their profile image. The path the image has upon upload to the system
        /// will be automatically recorded to effect the profile image change on the user's profile.
        /// </remarks>
        /// </summary>
        public required string ProfileImagePath { get; set; } = "images/profile/profile-placeholder-image.png";

        /// <summary>
        /// Property <c>ProfileCreatedAt</c> represents the date and time the profile was created.
        /// </summary>
        public required DateTime ProfileCreatedAt { get; set; }

        // NAVIGATIONAL PROPERTIES
        /// <summary>
        /// Property <c>AppUserId</c> represents the unique ID of the user who this given profile is associated with.
        /// <remarks>
        /// This property can never be null. A profile is automatically created for every user at the time of successful registration.
        /// </remarks>
        /// </summary>
        public required int AppUserId { get; set; }

        /// <summary>
        /// Property <c>ProfileOwner</c> represents the AppUser object that is associated with this profile.
        /// </summary>
        public required AppUser ProfileOwner { get; set; }
    }
}
