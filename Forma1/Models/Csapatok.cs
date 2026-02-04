using System;
using System.Collections.Generic;

namespace Forma1.Models;

public partial class Csapatok
{
    public int Csazon { get; set; }

    public string Csnev { get; set; } = null!;

    public virtual ICollection<Pilotak> Pilotaks { get; set; } = new List<Pilotak>();
}
