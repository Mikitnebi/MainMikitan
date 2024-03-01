using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant;

public class RestaurantStaffEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Length(5, 50)]
    public string FullNameGeo { get; set; } = null!;

    [Length(5, 50)]
    public string FullNameEng { get; set; } = null!;

    public bool IsManager { get; set; }

    [Length(5, 20)]
    public string PhoneNumber { get; set; } = null!;
  
    public bool PhoneConfirmation { get; set; }

    [Length(5, 50)] public string Email { get; set; } = null!;

    public bool EmailConfirmation { get; set; }

    public string UserNameHash { get; set; } = null!;
 
    public string PasswordHash { get; set; } = null!;

    public int RestaurantId { get; set; }
   
    public bool IsActive { get; set; }
    public int? UpdateUserId { get; set; }
    public DateTime? UpdateAt { get; set; }
    public int? CreateUserId { get; set; }
   
    public DateTime CreatedAt { get; set; }

}