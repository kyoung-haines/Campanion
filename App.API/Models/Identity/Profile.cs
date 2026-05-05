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
        public int ProfileId { get; set; }
        public required string ProfileImagePath { get; set; }
        public required DateTime ProfileCreatedAt { get; set; }

        // NAVIGATIONAL PROPERTIES
        public required int AppUserId { get; set; }
        public required AppUser ProfileOwner { get; set; }
    }
}
