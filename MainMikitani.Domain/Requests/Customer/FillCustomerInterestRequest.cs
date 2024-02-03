using System.ComponentModel.DataAnnotations;

namespace MainMikitan.Domain.Requests.Customer
{
    public record FillCustomerInterestRequest
    {
        [MaxLength(3)]
        public List<int> CousinsTypeIds { get; set; } = null!;
        [MaxLength(3)]
        public List<int> MusicsTypeIds { get; set; } = null!;
        [MaxLength(3)]
        public List<int> EnvironmentTypeIds { get; set; } = null!;
    }
}
