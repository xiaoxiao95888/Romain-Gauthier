namespace Romain_Gauthier.Test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TrainArticles", "Index", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TrainArticles", "Index");
        }
    }
}
