namespace App.API.Models
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Member = "User";

        public static List<string> AllRoles()
        {
            return new List<string> { "Admin", "User" };
        }
    }
}
