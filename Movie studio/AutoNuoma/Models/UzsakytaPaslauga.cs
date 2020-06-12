using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class UzsakytaPaslauga
    {
        public int fk_sutartis { get; set; }
        [Required]
        public DateTime fk_kainaGaliojaNuo { get; set; }
        [Required]
        public int fk_paslauga { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int kiekis { get; set; }
        public decimal kaina { get; set; }
        public virtual string fk_key {get;set;}
    }
}