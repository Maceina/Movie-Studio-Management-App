using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class SAtaskaitaViewModel
    {
        [DisplayName("Sutartis")]
        public int nr { get; set; }
        [DisplayName("Data")]
        public DateTime sutartiesData { get; set; }
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string asmensKodas { get; set; }
        [DisplayName("Sudarytų sutarčių vertė")]
        public decimal kaina { get; set; }
        [DisplayName("Užsakytų paslaugų vertė")]
        public decimal paslauguKaina { get; set; }
        public decimal bendraSuma { get; set; }
        public decimal bendraSumaPaslaug { get; set; }

    }
}