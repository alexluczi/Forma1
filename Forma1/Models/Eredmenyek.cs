using System;
using System.Collections.Generic;

namespace Forma1.Models;

public partial class Eredmenyek
{
    public int Pilota { get; set; }

    public string Nagydij { get; set; } = null!;

    public int? Startpoz { get; set; }

    public int? Celpoz { get; set; }

    public virtual Versenyek NagydijNavigation { get; set; } = null!;

    public virtual Pilotak PilotaNavigation { get; set; } = null!;
}
