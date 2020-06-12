using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoNuoma.ViewModels;

namespace AutoNuoma.ViewModels
{
    public class SutartisAtaskViewModel
    {
        public List<SAtaskaitaViewModel> sutartys { get; set; }
        public decimal visoSumaSutartciu { get; set; }
        public decimal visoSumaPaslauga { get; set; }
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ?nuo { get; set; }
        [DataType(DataType.Text)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ?iki { get; set; }
    }
}