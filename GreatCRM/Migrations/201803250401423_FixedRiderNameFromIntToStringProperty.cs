namespace GreatCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedRiderNameFromIntToStringProperty : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Riders", "RiderName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Riders", "RiderName", c => c.Int(nullable: false));
        }
    }
}
