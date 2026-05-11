using App.API.Enums;
using Microsoft.EntityFrameworkCore;
namespace App.API.Models.Social
{
    [PrimaryKey(nameof(SenderAppUserId), nameof(ReceiverAppUserId))]
    public class FriendRequest
    {
        public int SenderAppUserId { get; set; }
        public int ReceiverAppUserId { get; set; }
        public DateTime FriendRequestSentAt { get; set; }
        public DateTime FriendRequestRespondedAt { get; set; }
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
