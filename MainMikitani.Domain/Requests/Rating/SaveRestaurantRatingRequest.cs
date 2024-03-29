﻿namespace MainMikitan.Domain.Requests.Rating;

public class SaveRestaurantRatingRequest
{
    public int RestaurantId { get; set; }
    public int ReservationId { get; set; }
    public float OverallRatingRestaurant { get; set; }
    public float? OverallRatingApp { get; set; }
    public float? OverallDishRating { get; set; }
    public float? EnvironmentRating { get; set; }
    public float? ServiceRating { get; set; }
    public float? PriceRating { get; set; }
    public string? Comment { get; set; }
}