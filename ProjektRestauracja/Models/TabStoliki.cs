using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace ProjektRestauracja.DBC
{
    public class TabStoliki
    {
        [Key]
        public int Numer_stolika { get; set; }
        [Required]
        [DisplayName("Ilość Miejsc")]
        public int Ilość_miejsc { get; set; }
            
    }
}