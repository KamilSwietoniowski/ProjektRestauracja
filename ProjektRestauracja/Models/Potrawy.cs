using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProjektRestauracja.Models
{
    public class Potrawy
    {
        [Key]
        public int ID_Potrawy { get; set; }
        [DisplayName("Nazwa Potrawy:")]
        [Required]
        public string Nazwa_Potrawy { get; set; }
        [DisplayName("Cena:")]
        [Required]
        public decimal Cena { get; set; }
        [DisplayName("Typ Potrawy:")]
        [Required]
        public string Typ_Potrawy { get; set; }
    }
}