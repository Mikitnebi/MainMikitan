namespace MainMikitan.Domain.Models.Customer;
public class CustomerInfoEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int CustomerId { get; set; }
    public DateTime BirthDate { get; set; }
    public int GenderId { get; set; }
    public int NationalityId { get; set; }
    public DateTime? UpdateAt { get; set; }
    public DateTime CreateAt { get; set; }
}
    

