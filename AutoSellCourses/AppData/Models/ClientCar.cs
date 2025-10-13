using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AutoSellCourses.AppData.Models;

public partial class ClientCar
{
    public int ClientcarId { get; set; }

    public int ClientId { get; set; }

    public string CarBrand { get; set; } = null!;

    public int Mileage { get; set; }

    public DateTime ManufactureDate { get; set; }

    public string Description { get; set; } = null!;

    public string ColorCar { get; set; } = null!;

    public string TransmissionType { get; set; } = null!;

    public string EngineCapacity { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual Client? Client { get; set; } = null!;
}
