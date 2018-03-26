namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProposalAndRiderTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Proposals",
                c => new
                    {
                        ProposalId = c.Int(nullable: false, identity: true),
                        Insurance = c.String(),
                    })
                .PrimaryKey(t => t.ProposalId);
            
            CreateTable(
                "dbo.Riders",
                c => new
                    {
                        RiderId = c.Int(nullable: false, identity: true),
                        RiderName = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RiderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Riders");
            DropTable("dbo.Proposals");
        }
    }
}
