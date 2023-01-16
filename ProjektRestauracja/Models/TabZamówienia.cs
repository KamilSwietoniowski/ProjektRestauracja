using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ProjektRestauracja.Models
{
    public class TabZamówienia
    {
        [Key]
        public int ID_Zamówienia { get; set; }
        [DisplayName("ID_Potrawy:")]
        [Required]
        public int ID_Potrawy { get; set; }
        [DisplayName("ID_Pracownika:")]
        [Required]
        public int ID_Pracownika { get; set; }
        [DisplayName("Status Zamówienia:")]
        [Required]
        public string Status_Zamówienia { get; set; }
        [DisplayName("Numer_Stolika:")]
        [Required]
        public int Numer_Stolika { get; set; }
        
      
    }
}