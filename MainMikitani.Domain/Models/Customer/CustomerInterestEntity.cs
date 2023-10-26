namespace MainMikitan.Domain.Models.Customer;

public class CustomerInterestEntity
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int InterestId { get; set; }
    public int CreatedAt { get; set; }
}