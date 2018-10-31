using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Document { get; set; }

        public string Name { get; set; }

        public string SurName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string AuthorId { get; set; }
        public virtual ApplicationUser Author { get; set; }

    }
}
