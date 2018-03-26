namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedManyToMany5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposalRiders", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProposalRiders", "Note");
        }
    }
}
