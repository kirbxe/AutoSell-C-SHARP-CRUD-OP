    using System;
using System.Collections.Generic;

namespace AutoSellCourses.AppData.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string ClientLastName { get; set; } = null!;

    public string? ClientName { get; set; }

    public string ClientMiddleName { get; set; } = null!;

    public string ClientTown { get; set; } = null!;

    public string ClientAddress { get; set; } = null!;

    public string ClientNumber { get; set; } = null!;

    public virtual ICollection<ClientCar> ClientCars { get; set; } = new List<ClientCar>();

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
