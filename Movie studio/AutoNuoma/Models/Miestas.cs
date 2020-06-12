using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Miestas
    {
        public int id { get; set; }
        public string pavadinimas { get; set; }

        public virtual ICollection<Aikstele> Aiksteles { get; set; }
    }
}