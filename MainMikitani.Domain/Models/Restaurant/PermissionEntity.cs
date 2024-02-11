namespace MainMikitan.Domain.Models.Restaurant;

public record PermissionEntity
{
    public int Id { get; set; }
    public int StaffId { get; set; }
    public int PermissionId { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdaterStaffId { get; set; }
}