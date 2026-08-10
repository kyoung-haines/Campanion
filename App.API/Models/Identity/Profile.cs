using Campanion.Shared.Dtos.ProfileDtos;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.API.Models.Identity
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
        /// Property <c>ProfileUsername</c> represents the custom, user-defined username that is displayed 
        /// on their profile page and is visible to the public.
        /// </summary>
        private string _profileUsername = string.Empty;
        public string ProfileUsername 
        {
            get => _profileUsername;
            set => _profileUsername = value;
        }

        /// <summary>
        /// Property <c>ProfileImagePath</c> represents the local path to the user profile's profile image.
        /// <remarks>
        /// This will default to the placeholder profile image upon profile creation.
        /// <br />
        /// Users can upload an image to be used as their profile image. The path the image has upon upload to the system
        /// will be automatically recorded to effect the profile image change on the user's profile.
        /// </remarks>
        /// </summary>
        public string ProfileImagePath { get; set; } = "images/profile/profile-placeholder-image.png";

        /// <summary>
        /// Property <c>ProfileCreatedAt</c> represents the date and time the profile was created.
        /// </summary>
        public DateTime ProfileCreatedAt { get; set; } = DateTime.Now;

        // NAVIGATIONAL PROPERTIES
        /// <summary>
        /// Property <c>AppUserId</c> represents the unique ID of the user who this given profile is associated with.
        /// <remarks>
        /// This property can never be null. A profile is automatically created for every user at the time of successful registration.
        /// </remarks>
        /// </summary>
        public string AppUserId { get; set; }

        /// <summary>
        /// Property <c>ProfileOwner</c> represents the AppUser object that is associated with this profile.
        /// </summary>
        [ForeignKey(nameof(AppUserId))]
        public AppUser ProfileOwner { get; set; }


        // HELPER METHOD(S)
        public async Task<ProfileResponseDto> ConvertProfileObjectToResponseDto(Profile profile)
        {
            ProfileResponseDto profileResponseDto = new ProfileResponseDto
            {
                ProfileId = this.ProfileId.ToString(),
                ProfileUsername = this.ProfileUsername,
                ProfileImagePath = this.ProfileImagePath,
                ProfileCreatedAt = this.ProfileCreatedAt.ToString(),
                AppUserId = this.AppUserId,
                ProfileUserProvince = this.ProfileOwner.AppUserProvince,
                ProfileUserCountry = this.ProfileOwner.AppUserCountry
            };

            return profileResponseDto;
        }
    }
}
