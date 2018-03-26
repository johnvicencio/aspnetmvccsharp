namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Multi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalRiders", "ProposalRiderId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProposalRiders", "ProposalRiderId");
        }
    }
}
