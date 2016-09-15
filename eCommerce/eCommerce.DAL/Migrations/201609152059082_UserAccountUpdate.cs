namespace eCommerce.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAccountUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "CustomerF_Name", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "CustomerL_Name", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "Password", c => c.String(nullable: false));
            AddColumn("dbo.Customers", "ConfirmPassword", c => c.String());
            AlterColumn("dbo.Customers", "Address1", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "Town", c => c.String(nullable: false));
            AlterColumn("dbo.Customers", "PostCode", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerName", c => c.String());
            AlterColumn("dbo.Customers", "PostCode", c => c.String());
            AlterColumn("dbo.Customers", "Town", c => c.String());
            AlterColumn("dbo.Customers", "Address1", c => c.String());
            DropColumn("dbo.Customers", "ConfirmPassword");
            DropColumn("dbo.Customers", "Password");
            DropColumn("dbo.Customers", "Email");
            DropColumn("dbo.Customers", "CustomerL_Name");
            DropColumn("dbo.Customers", "CustomerF_Name");
        }
    }
}
