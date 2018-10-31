namespace BillApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserToTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "AuthorId", c => c.String(maxLength: 128));
            AddColumn("dbo.InvoiceHeaders", "AuthorId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "AuthorId");
            CreateIndex("dbo.InvoiceHeaders", "AuthorId");
            AddForeignKey("dbo.Customers", "AuthorId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.InvoiceHeaders", "AuthorId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceHeaders", "AuthorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "AuthorId", "dbo.AspNetUsers");
            DropIndex("dbo.InvoiceHeaders", new[] { "AuthorId" });
            DropIndex("dbo.Customers", new[] { "AuthorId" });
            DropColumn("dbo.InvoiceHeaders", "AuthorId");
            DropColumn("dbo.Customers", "AuthorId");
        }
    }
}
