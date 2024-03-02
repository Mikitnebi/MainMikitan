using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Crypto.Engines;
using System.ComponentModel.DataAnnotations;
using MainMikitan.Domain;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Domain.Requests.RestaurantRequests;

public class RestaurantInfoRequest
{
    public decimal LocationX { get; set; }
    public decimal LocationY { get; set; }

    [MaxLength(40,ErrorMessage = ErrorResponseType.MoreThanMaxSizeInput),
        MinLength(5, ErrorMessage = ErrorResponseType.NotEnoughInput)]
    public string Address { get; set; } = null!;
    
    [MaxLength(40,ErrorMessage = ErrorResponseType.MoreThanMaxSizeInput),
     MinLength(5, ErrorMessage = ErrorResponseType.NotEnoughInput)]
    public string AddressEng { get; set; } = null!;

    //Required to change BusinessTypeSize each time its size changes BY HAND
    [Range(0, BusinessTypeSize, ErrorMessage = ErrorResponseType.WrongBusinessTypeId)]
    public int BusinessTypeId { get; set; }
    public int PriceTypeId { get; set; }
    public int RegionId { get; set; }

    public bool HasCoupe { get; set; }
    public bool HasTerrace { get; set; }

    [Required]
    public TimeOnly HallStartTime { get; init; }
    [Required]
    public TimeOnly HallEndTime { get; init; }
    [Required]
    public TimeOnly KitchenStartTime { get; init; }
    [Required]
    public TimeOnly KitchenEndTime { get; init; }
    [Required]
    public TimeOnly MusicStartTime { get; init; }
    [Required]
    public TimeOnly MusicEndTime { get; init; }
    public int ActiveStatusId { get; set; }
    public bool ForCorporateEvents { get; set; }
    
    [MaxLength(250, ErrorMessage = ErrorResponseType.MoreThanMaxSizeInput), 
        MinLength(10, ErrorMessage = ErrorResponseType.NotEnoughInput)]
    public string? DescriptionGeo { get; set; }
    [MaxLength(250, ErrorMessage = ErrorResponseType.MoreThanMaxSizeInput), 
        MinLength(10, ErrorMessage = ErrorResponseType.NotEnoughInput)]
    public string? DescriptionEng { get; set; }
}
