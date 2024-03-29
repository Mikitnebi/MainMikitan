﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMikitan.Domain.Models.Restaurant.TableManagement;

public class TableInfoEntity
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int TableNumber { get; set; }
    public int MaxPlace { get; set; }
    public int MinPlace { get; set; }
    public int TableType { get; set; }
    public int FloorNumber { get; set; }
    public decimal XCoordinate { get; set; }
    public decimal YCoordinate { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public bool ActiveForReservation { get; set; }
    public DateTime CreatedAt { get; set; }
}