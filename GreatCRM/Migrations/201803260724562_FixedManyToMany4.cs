namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedManyToMany4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProposalRiders", "ProposalRiderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProposalRiders", "ProposalRiderId", c => c.Int(nullable: false));
        }
    }
}
