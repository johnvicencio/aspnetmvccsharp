namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedManyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RiderProposals", "Rider_RiderId", "dbo.Riders");
            DropForeignKey("dbo.RiderProposals", "Proposal_ProposalId", "dbo.Proposals");
            DropIndex("dbo.RiderProposals", new[] { "Rider_RiderId" });
            DropIndex("dbo.RiderProposals", new[] { "Proposal_ProposalId" });
            AddColumn("dbo.Proposals", "Rider_RiderId", c => c.Int());
            CreateIndex("dbo.Proposals", "Rider_RiderId");
            AddForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders", "RiderId");
            DropTable("dbo.RiderProposals");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RiderProposals",
                c => new
                    {
                        Rider_RiderId = c.Int(nullable: false),
                        Proposal_ProposalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rider_RiderId, t.Proposal_ProposalId });
            
            DropForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.Proposals", new[] { "Rider_RiderId" });
            DropColumn("dbo.Proposals", "Rider_RiderId");
            CreateIndex("dbo.RiderProposals", "Proposal_ProposalId");
            CreateIndex("dbo.RiderProposals", "Rider_RiderId");
            AddForeignKey("dbo.RiderProposals", "Proposal_ProposalId", "dbo.Proposals", "ProposalId", cascadeDelete: true);
            AddForeignKey("dbo.RiderProposals", "Rider_RiderId", "dbo.Riders", "RiderId", cascadeDelete: true);
        }
    }
}
