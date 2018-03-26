namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProposalManyToManyRelationshipWithRider : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProposalRiders",
                c => new
                    {
                        ProposalId = c.Int(nullable: false),
                        RiderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProposalId, t.RiderId })
                .ForeignKey("dbo.Proposals", t => t.ProposalId, cascadeDelete: true)
                .ForeignKey("dbo.Riders", t => t.RiderId, cascadeDelete: true)
                .Index(t => t.ProposalId)
                .Index(t => t.RiderId);
            
            CreateTable(
                "dbo.RiderProposals",
                c => new
                    {
                        Rider_RiderId = c.Int(nullable: false),
                        Proposal_ProposalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Rider_RiderId, t.Proposal_ProposalId })
                .ForeignKey("dbo.Riders", t => t.Rider_RiderId, cascadeDelete: true)
                .ForeignKey("dbo.Proposals", t => t.Proposal_ProposalId, cascadeDelete: true)
                .Index(t => t.Rider_RiderId)
                .Index(t => t.Proposal_ProposalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProposalRiders", "RiderId", "dbo.Riders");
            DropForeignKey("dbo.ProposalRiders", "ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.RiderProposals", "Proposal_ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.RiderProposals", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.RiderProposals", new[] { "Proposal_ProposalId" });
            DropIndex("dbo.RiderProposals", new[] { "Rider_RiderId" });
            DropIndex("dbo.ProposalRiders", new[] { "RiderId" });
            DropIndex("dbo.ProposalRiders", new[] { "ProposalId" });
            DropTable("dbo.RiderProposals");
            DropTable("dbo.ProposalRiders");
        }
    }
}
