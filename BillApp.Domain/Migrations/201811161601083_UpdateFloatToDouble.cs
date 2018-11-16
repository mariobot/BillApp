namespace BillApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFloatToDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.InvoiceItems", "Quanty", c => c.Double(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ValueUnit", c => c.Double(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ValueTotal", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "Total", c => c.Double(nullable: false));
            AlterColumn("dbo.Invoices", "Tax", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Invoices", "Tax", c => c.Single(nullable: false));
            AlterColumn("dbo.Invoices", "Total", c => c.Single(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ValueTotal", c => c.Single(nullable: false));
            AlterColumn("dbo.InvoiceItems", "ValueUnit", c => c.Single(nullable: false));
            AlterColumn("dbo.InvoiceItems", "Quanty", c => c.Single(nullable: false));
        }
    }
}
