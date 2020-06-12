using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;

namespace AutoNuoma.ViewModels
{
    public class PslgAtaskaitaViewModel
    {
        public List<PaslauguAtaskaitaViewModel> paslaugos { get; set; }
        public int visoUzsakyta { get; set; }
        public decimal bendraSuma { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy}",ApplyFormatInEditMode = true)]
        public DateTime? nuo { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? iki { get; set; }
    }
}