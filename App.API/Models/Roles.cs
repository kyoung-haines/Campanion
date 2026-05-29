namespace App.API.Models
{
    public static class Roles
    {
        public const string Admin = "ADMINISTRATOR";
        public const string Member = "REGULAR_USER";

        public static List<string> AllRoles()
        {
            var roles = new List<string> { "ADMINISTRATOR", "REGULAR_USER" };
            
            return roles;
        }
    }
}
