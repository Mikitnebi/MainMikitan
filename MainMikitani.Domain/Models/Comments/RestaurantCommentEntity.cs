namespace MainMikitan.Domain.Models.Comments;

public class RestaurantCommentEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public int ReservationId { get; set; }
    public int CommentTagId { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}