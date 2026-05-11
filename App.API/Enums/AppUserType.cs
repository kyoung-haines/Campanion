namespace App.API.Enums
{
    /// <summary>
    /// Enumeration <c>AppUserType</c> represents the type of account a given user holds.
    /// <remarks>
    /// AppUserType determines the user's access to application functionality. 
    /// <br />
    /// A user account is required to interact with the app in any capacity, and the default 
    /// account type upon new user registration is <see cref="REGULAR_USER"/> to satisfy the required condition 
    /// for this property. Should a user account need admin privilges, the account type will need to be
    /// changed to <see cref="ADMINISTRATOR"/> manually by a current admin.
    /// </remarks>
    /// </summary>   
    public enum AppUserType
    {
        /// <summary>
        /// Value <c>REGULAR_USER</c> represents the default user type that all users receive upon registration.
        /// <br />
        /// This AppUserType allows users to use the application in its total functionality, minus administrator functions.
        /// </summary>
        REGULAR_USER,

        /// <summary>
        /// Value <c>ADMINISTRATOR</c> represents an account type with elevated permissions level over the REGULAR_USER type.
        /// <remarks>
        /// It should be noted that in order to receive this AppUserType, manual admin intervention will be needed.
        /// </remarks>
        /// </summary>
        ADMINISTRATOR
    }
}
