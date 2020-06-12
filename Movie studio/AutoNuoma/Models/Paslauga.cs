using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Paslauga
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Pavadinimas")]
        [Required]
        public string pavadinimas { get; set; }
        [DisplayName("Aprašymas")]
        public string  aprasymas { get; set; }
        //Sąrašas paslaugos kainoms
        public virtual IEnumerable<PaslaugosKaina> kainos { get; set; }
    }
}