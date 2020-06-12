using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AutoNuoma.ViewModels
{
    public class SutartisListViewModel
    {
        [DisplayName("Sutarties Nr.")]
        public int nr { get; set; }
        [DisplayName("Sudarymo data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime data { get; set; }
        [DisplayName("Darbuotojas")]
        public string darbuotojas { get; set; }
        [DisplayName("Nuomininkas")]
        public string nuomininkas { get; set; }
        [DisplayName("Būsena")]
        public string busena { get; set; }
    }
}