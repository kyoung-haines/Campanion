namespace App.API.Models.AppUser
{
    /// <summary>
    /// Class <c>AppUser</c> represents a user of the application.
    /// <remarks>
    /// There are two types of users: <i>regular users</i> and <i>administrators.</i>
    /// </remarks>
    /// <returns>No return value.</returns>
    /// </summary>
    public class AppUser
    {
        /// <value>
        /// Property <c>AppUserId</c> represents the unique integer ID for a given user.
        /// </value>
        public int AppUserId { get; set; }

        /// <value>
        /// Property <c>AppUserType</c> represents the type of the user.
        /// <remarks>
        /// Users can be one of two types: regular user (<c>user</c>), or administrator (<c>admin</c>).
        /// </remarks>
        /// </value>
        
    }
}
