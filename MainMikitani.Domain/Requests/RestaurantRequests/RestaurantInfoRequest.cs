using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Crypto.Engines;
using System.ComponentModel.DataAnnotations;
using MainMikitan.Domain;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Domain.Requests.RestaurantRequests;

public class RestaurantInfoRequest
{
    public double LocationX { get; set; }
    public double LocationY { get; set; }

    [MaxLength(40,ErrorMessage = ErrorType.MoreThanMaxSizeInput),
        MinLength(5, ErrorMessage = ErrorType.NotEnoughInput)]
    public string Address { get; set; } = null!;

    //Required to change BusinessTypeSize each time its size changes BY HAND
    [Range(0, BusinessTypeSize, ErrorMessage = ErrorType.WrongBusinessTypeId)]
    public int BusinessTypeId { get; set; }
    public int PriceTypeId { get; set; }

    public bool HasCoupe { get; set; }
    public bool HasTerrace { get; set; }

    [Range(0,24, ErrorMessage = ErrorType.WrongHour)]
    public short HallStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short HallEndHour { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short HallStartMinute { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short HallEndMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short KitchenStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short KitchenEndHour { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short KitchenStartMinute { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short KitchenEndMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short MusicStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short MusicEndHour { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short MusicStartMinute { get; set; }

    [Range(0, 59, ErrorMessage = ErrorType.WrongMinute)]
    public short MusicEndMinute { get; set; }
    public bool ForCorporateEvents { get; set; }

    [MaxLength(250, ErrorMessage = ErrorType.MoreThanMaxSizeInput), 
        MinLength(50, ErrorMessage = ErrorType.NotEnoughInput)]
    public string? DescriptionGeo { get; set; }
    [MaxLength(250, ErrorMessage = ErrorType.MoreThanMaxSizeInput), 
        MinLength(50, ErrorMessage = ErrorType.NotEnoughInput)]
    public string? DescriptionEng { get; set; }
}
