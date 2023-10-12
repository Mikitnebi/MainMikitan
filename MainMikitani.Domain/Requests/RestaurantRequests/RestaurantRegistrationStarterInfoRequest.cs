using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Crypto.Engines;
using System.ComponentModel.DataAnnotations;
using MainMikitan.Domain;
using static MainMikitan.Domain.Enums;

namespace MainMikitan.Domain.Requests.RestaurantRequests;

public class RestaurantRegistrationStarterInfoRequest
{
    public double LocationX { get; set; }
    public double LocationY { get; set; }

    [MaxLength(40,ErrorMessage = ErrorType.MoreThanMaxSizeInput),
        MinLength(5, ErrorMessage = ErrorType.NotEnoughInput)]
    public string Address { get; set; } = null!;

    //Required to change BusinessTypeSize each time its size changes BY HAND
    [Range(0, BusinessTypeSize, ErrorMessage = ErrorType.WrongBusinessTypeId)]
    public BusinessType BusinessTypeId { get; set; }

    public int CoupeQuantity { get; set; }
    public int TableQuantity { get; set; }
    public int TerraceQuantity { get; set; }

    [Range(0,24, ErrorMessage = ErrorType.WrongHour)]
    public short HallStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short HallEndHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short HallStartMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short HallEndMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short KitchenStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short KitchenEndHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short KitchenStartMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short KitchenEndMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short MusicStartHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongHour)]
    public short MusicEndHour { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short MusicStartMinute { get; set; }

    [Range(0, 24, ErrorMessage = ErrorType.WrongMinute)]
    public short MusicEndMinute { get; set; }
    public bool ForCorporateEvents { get; set; }

    [MaxLength(250, ErrorMessage = ErrorType.MoreThanMaxSizeInput), 
        MinLength(50, ErrorMessage = ErrorType.NotEnoughInput)]
    public string? Description { get; set; }

    [Range(0, 2, ErrorMessage = ErrorType.WrongRestaurantActiveStatusId)]
    public RestaurantActiveStatus ActiveStatusId { get; set; }

    public bool TwoStepAuth { get; set; }
}
