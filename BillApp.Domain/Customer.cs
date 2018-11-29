using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [DataType(DataType.Text)]
        public string Document { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        public string Name { get; set; }

        public string SurName { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [EmailAddress(ErrorMessage ="No tiene formato de Email")]
        public string Email { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

    }
}
