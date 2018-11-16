using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class InvoiceItem
    {
        public int id { get; set; }

        public string item { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Range(1,9999,ErrorMessage ="Valor fuera del rango 1 - 9999")]        
        public double Quanty { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        public double ValueUnit { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo numeros")]
        public double ValueTotal { get; set; }

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
