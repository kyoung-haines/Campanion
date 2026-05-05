using App.API.Enums;
using App.API.Models.Campgrounds;

namespace App.API.Models.AppUser
{
    /// <summary>
    /// Class <c>AppUser</c> represents a user of the application.
    /// <remarks>
    /// There are two types of users: <i>regular users</i> and <i>administrators.</i>
    /// <br />
    /// When a user registers for a new account, some AppUser properties are required, while others are not.
    /// Those properties of which are required, are clearly indicated.
    /// <br />
    /// The AppUserType property is not a selectable option. Instead, this will default to <see cref="AppUserType.REGULAR_USER">REGULAR_USER</see>
    /// <br />
    /// AppUser identity is handled by the ASP.NET Core Identity framework for safe storage and retrieval of user details.
    /// </remarks>
    /// <returns>No return value.</returns>
    /// </summary>
    public class AppUser
    {
        /// <summary>
        /// Property <c>AppUserId</c> represents the unique integer ID for a given user.
        /// </summary>
        public int AppUserId { get; set; }

        /// <summary>
        /// Property <c>AppUserType</c> represents the type of account a given user holds.
        /// <remarks>
        /// Users can be one of two types: regular user (<i>user</i>), or administrator (<i>admin</i>).
        /// <br />
        /// The account type determines the level of access to the application the user has. General application use requires a user account
        /// while broader functionality is restricted behind <i>admin</i> access.
        /// <br />
        /// Default value is <see cref="AppUserType.REGULAR_USER"/>
        /// </remarks>
        /// </summary>
        public AppUserType AppUserType { get; set; } = AppUserType.REGULAR_USER;

        /// <summary>
        /// Property <c>AppUserUsername</c> represents the username of a given user.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserUsername { get; set; }

        /// <summary>
        /// Property <c>AppUserPassword</c> represents the password for a given user.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserPassword { get; set; }

        /// <summary>
        /// Property <c>AppUserEmail</c> represents the email for a given user.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserEmail { get; set; }

        /// <summary>
        /// Property <c>AppUserPhone</c> represents the phone number of a given user.
        /// <remarks>
        /// This field is required.
        /// </remarks>
        /// </summary>
        public required string AppUserPhone { get; set; }

        /// <summary>
        /// Property <c>AppUserFirstName</c> represents the First Name of a given user.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserFirstName { get; set; }

        /// <summary>
        /// Property <c>AppUserLastName</c> represents the Last Name of a given user.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserLastName { get; set; }

        /// <summary>
        /// Property <c>AppUserStreetAddress</c> represents the street and street number portion of a given user's address.
        /// </summary>
        public string? AppUserStreetAddress { get; set; }

        /// <summary>
        /// Property <c>AppUserCity</c> represents the city portion of a given user's address.
        /// </summary>
        public string? AppUserCity { get; set; }

        /// <summary>
        /// Property <c>AppUserProvince</c> represents the province portion of a given user's address.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required int AppUserProvince { get; set; }

        /// <summary>
        /// Property <c>AppUserCountry</c> represents the country portion of a given user's address.
        /// <remarks>
        /// This property is required.
        /// </remarks>
        /// </summary>
        public required string AppUserCountry { get; set; }

        /// <summary>
        /// Property <c>AppUserPostalCode</c> represents the postal code (or regional equivalent) for a given user's adress.
        /// </summary>
        public string? AppUserPostalCode { get; set; }

        // NAVIGATIONAL PROPERTIES
        /// <summary>
        /// Property <c>AppUserFavouriteCampgrounds</c> represents the navigational relationship that EF Core uses to associate a user
        /// with a list of their favourited campgrounds.
        /// </summary>
        public List<Campground>? AppUserFavouriteCampgrounds { get; set; }

        /// <summary>
        /// Property <c>AppUserProfileId</c> represents the unique ID for the given user's profile object.
        /// </summary>
        public int AppUserProfileId { get; set; }

        /// <summary>
        /// Property <c>AppUserProfile</c> represents the profile object that is associated with a given user. 
        /// <remarks>
        /// This property is required.
        /// <br />
        /// However, it should be noted that a user profile is automatically created for every user at their time of registration,
        /// so this should always be satisfied.
        /// </remarks>
        /// </summary>
        public required Profile AppUserProfile { get; set; }
    }
}
