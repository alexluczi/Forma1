using System;
using System.Collections.Generic;

namespace Forma1.Models;

public partial class Pilotak
{
    public int Pazon { get; set; }

    public string Pnev { get; set; } = null!;

    public int? Szev { get; set; }

    public int? Csapat { get; set; }

    public virtual Csapatok? CsapatNavigation { get; set; }

    public virtual ICollection<Eredmenyek> Eredmenyeks { get; set; } = new List<Eredmenyek>();
}
