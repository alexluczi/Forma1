using System;
using System.Collections.Generic;

namespace Forma1.Models;

public partial class Versenyek
{
    public string Vkod { get; set; } = null!;

    public DateTime Datum { get; set; }

    public string Vnev { get; set; } = null!;

    public string Hely { get; set; } = null!;

    public int Kor { get; set; }

    public int Hossz { get; set; }

    public virtual ICollection<Eredmenyek> Eredmenyeks { get; set; } = new List<Eredmenyek>();
}
