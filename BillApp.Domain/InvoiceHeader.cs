using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class InvoiceHeader
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        public string Identification { get; set; }

        public string Adress { get; set; }

        public string Phone { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Range(1, 999999, ErrorMessage = "Valor fuera del rango 1 - 999999")]
        public int Sequence { get; set; }
        
        public string Prefix { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

    }
}
