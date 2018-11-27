namespace BillApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Document", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Phone", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.InvoiceHeaders", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.InvoiceHeaders", "Identification", c => c.String(nullable: false));
            AlterColumn("dbo.InvoiceHeaders", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.InvoiceHeaders", "Email", c => c.String());
            AlterColumn("dbo.InvoiceHeaders", "Identification", c => c.String());
            AlterColumn("dbo.InvoiceHeaders", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Email", c => c.String());
            AlterColumn("dbo.Customers", "Phone", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Document", c => c.String());
        }
    }
}
