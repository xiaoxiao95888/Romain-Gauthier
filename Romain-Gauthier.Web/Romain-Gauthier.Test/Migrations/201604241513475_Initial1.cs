namespace Romain_Gauthier.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "ExternalUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "ExternalUrl");
        }
    }
}
