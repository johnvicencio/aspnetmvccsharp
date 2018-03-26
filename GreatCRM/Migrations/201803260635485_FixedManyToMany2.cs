namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedManyToMany2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders");
            AddColumn("dbo.Proposals", "Rider_RiderId1", c => c.Int());
            AddColumn("dbo.Riders", "Proposal_ProposalId", c => c.Int());
            CreateIndex("dbo.Proposals", "Rider_RiderId1");
            CreateIndex("dbo.Riders", "Proposal_ProposalId");
            AddForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders", "RiderId");
            AddForeignKey("dbo.Riders", "Proposal_ProposalId", "dbo.Proposals", "ProposalId");
            AddForeignKey("dbo.Proposals", "Rider_RiderId1", "dbo.Riders", "RiderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Proposals", "Rider_RiderId1", "dbo.Riders");
            DropForeignKey("dbo.Riders", "Proposal_ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.Riders", new[] { "Proposal_ProposalId" });
            DropIndex("dbo.Proposals", new[] { "Rider_RiderId1" });
            DropColumn("dbo.Riders", "Proposal_ProposalId");
            DropColumn("dbo.Proposals", "Rider_RiderId1");
            AddForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders", "RiderId");
        }
    }
}
