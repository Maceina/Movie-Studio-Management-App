using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.Models
{
    public class Klientas
    {
        [DisplayName("Fimo ID")]
        [Required]
        public string asmensKodas { get; set; }
        [DisplayName("Pavadinimas")]
        [Required]
        public string vardas { get; set; }
        [DisplayName("Išleidimo Metai")]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Biudžetas (eur.)")]
        [Required]
        public string gimimoData { get; set; }
        [DisplayName("Pajamos (eur.)")]
        [Required]
        public string telefonas { get; set; }
        [DisplayName("Filmo Būsena")]
        [Required]
        public string epastas { get; set; }
    }
}