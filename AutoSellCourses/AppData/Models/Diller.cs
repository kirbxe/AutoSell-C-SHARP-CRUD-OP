using System;
using System.Collections.Generic;

namespace AutoSellCourses.AppData.Models;

public partial class Diller
{
    public int DillersId { get; set; }

    public string DillersLastName { get; set; } = null!;

    public string? DillersName { get; set; }

    public string DillersMiddleName { get; set; } = null!;

    public string DillersTown { get; set; } = null!;

    public string DillersAddress { get; set; } = null!;

    public string DillersNumber { get; set; } = null!;

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
