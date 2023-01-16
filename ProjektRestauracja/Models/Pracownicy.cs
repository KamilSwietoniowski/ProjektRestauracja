using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektRestauracja.Models
{
    public class Pracownicy
    {
        [Key]
        public int ID_Pracownika { get; set; }
        [DisplayName("Imię:")]
        [Required]
        public string Imię { get; set; }
        [DisplayName("Nazwisko:")]
        [Required]
        public string Nazwisko { get; set; }
        [DisplayName("Stanowisko:")]
        [Required]
        public string Stanowisko { get; set; }
        [DisplayName("Data Zatrudnienia:")]
        [Required]
        public DateTime Data_Zatrudnienia { get; set; }
        [DisplayName("Pensja:")]
        [Required]
        public decimal Pensja { get; set; }
        [DisplayName("Płeć:")]
        [Required]
        public string Płeć { get; set; }
        

    }
}