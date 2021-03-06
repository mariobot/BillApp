﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillApp.Domain.Interface
{
    public interface IInvoiceHeader
    {
        void AddInvoiceHeader(InvoiceHeader _invoiceH);

        InvoiceHeader GetInvoiceHeaderById(string userid,int id);

        InvoiceHeader GetInvoiceHeaderByUser(string userId);

        void UpdateInvoiceHeader(InvoiceHeader _invoiceH);

        void DeleteInvoiceHeader(string userid, int id);

        void Dispose();
        
    }
}
