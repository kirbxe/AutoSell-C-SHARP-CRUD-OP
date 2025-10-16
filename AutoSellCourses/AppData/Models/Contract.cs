using System;
using System.Collections.Generic;

namespace AutoSellCourses.AppData.Models;

public partial class Contract
{
    public int ContractId { get; set; }

    public int ClientId { get; set; }

    public int? DillerId { get; set; }

    public DateTime ContractDate { get; set; }

    public string CarBrand { get; set; } = null!;

    public string? CarPhoto { get; set; }

    public DateTime ManufactureDate { get; set; }

    public int Mileage { get; set; }

    public DateOnly? SaleDate { get; set; }

    public decimal SalePrice { get; set; }

    public decimal Commission { get; set; }

    public string? Notes { get; set; }

    public virtual Client? Client { get; set; } = null!;

    public virtual Diller? Diller { get; set; } = null!;
}
