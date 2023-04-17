using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LAB04_ED1.Models
{
    public class Vehiculos
    {
        [Display(Name = "ID")]
        [Required]
        public string ID { get; set; }
        [Display(Name = "Placa")]
        [Required]
        public string Placa { get; set; }

        [Display(Name = "Color")]
        [Required]
        public string Color { get; set; }

        [Display(Name = "Propietario")]
        [Required]
        public string Propietario { get; set; }

        [Display(Name = "Longitud")]
        [Required]
        public string Longitud { get; set;}

        [Display(Name = "Latitud")]
        [Required]
        public string Latitud { get; set; }



    }
}
