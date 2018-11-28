using BillApp.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BillApp.Web.Models
{
    public class InvoiceViewModels
    {
        public Invoice Invoice { get; set; }
        public InvoiceItem InvoiceItem { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set;}
    }

    public class SeachViewModels
    {
        [Required(ErrorMessage ="* Requerido")]
        [DataType(DataType.Date,ErrorMessage ="Formato Incorrecto")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime startDate { get; set; }

        [Required(ErrorMessage = "* Requerido")]
        [DataType(DataType.Date, ErrorMessage = "Formato Incorrecto")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime endDate { get; set; }
    }

    public class SeachInvoiceViewModels
    {
        public List<Invoice> Incoices { get; set; }
        public SeachViewModels SearchVM { get; set; }
    }
}