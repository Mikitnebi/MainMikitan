namespace MainMikitan.Domain.Requests.Comment;

public class CreateRestaurantCommentRequest
{
    public int RestaurantId { get; set; }
    public int ReservationId { get; set; }
    public int CommentTagId { get; set; }
    public string? Comment { get; set; }
}