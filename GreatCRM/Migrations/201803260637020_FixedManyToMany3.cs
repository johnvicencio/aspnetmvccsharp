namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedManyToMany3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders");
            DropForeignKey("dbo.Proposals", "Rider_RiderId1", "dbo.Riders");
            DropForeignKey("dbo.Riders", "Proposal_ProposalId", "dbo.Proposals");
            DropIndex("dbo.Proposals", new[] { "Rider_RiderId" });
            DropIndex("dbo.Proposals", new[] { "Rider_RiderId1" });
            DropIndex("dbo.Riders", new[] { "Proposal_ProposalId" });
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
            
            DropColumn("dbo.Proposals", "Rider_RiderId");
            DropColumn("dbo.Proposals", "Rider_RiderId1");
            DropColumn("dbo.Riders", "Proposal_ProposalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Riders", "Proposal_ProposalId", c => c.Int());
            AddColumn("dbo.Proposals", "Rider_RiderId1", c => c.Int());
            AddColumn("dbo.Proposals", "Rider_RiderId", c => c.Int());
            DropForeignKey("dbo.RiderProposals", "Proposal_ProposalId", "dbo.Proposals");
            DropForeignKey("dbo.RiderProposals", "Rider_RiderId", "dbo.Riders");
            DropIndex("dbo.RiderProposals", new[] { "Proposal_ProposalId" });
            DropIndex("dbo.RiderProposals", new[] { "Rider_RiderId" });
            DropTable("dbo.RiderProposals");
            CreateIndex("dbo.Riders", "Proposal_ProposalId");
            CreateIndex("dbo.Proposals", "Rider_RiderId1");
            CreateIndex("dbo.Proposals", "Rider_RiderId");
            AddForeignKey("dbo.Riders", "Proposal_ProposalId", "dbo.Proposals", "ProposalId");
            AddForeignKey("dbo.Proposals", "Rider_RiderId1", "dbo.Riders", "RiderId");
            AddForeignKey("dbo.Proposals", "Rider_RiderId", "dbo.Riders", "RiderId");
        }
    }
}
