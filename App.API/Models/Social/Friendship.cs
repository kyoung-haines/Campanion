using Microsoft.EntityFrameworkCore;

namespace App.API.Models.Social
{
    [PrimaryKey(nameof(AppUserId1), nameof(AppUserId2))]
    public class Friendship
    {
        public int AppUserId1 { get; set; }
        public int AppUserId2 { get; set; }
    }
}
