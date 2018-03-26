namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClientOneToManyProposals : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Proposals", "ClientId", c => c.Int(nullable: false));
            CreateIndex("dbo.Proposals", "ClientId");
            AddForeignKey("dbo.Proposals", "ClientId", "dbo.Clients", "ClientId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proposals", "ClientId", "dbo.Clients");
            DropIndex("dbo.Proposals", new[] { "ClientId" });
            DropColumn("dbo.Proposals", "ClientId");
        }
    }
}
