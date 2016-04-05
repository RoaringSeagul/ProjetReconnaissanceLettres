namespace TPARCHIPERCEPTRON.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPerceptronINBd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perceptron",
                c => new
                    {
                        PerceptronID = c.Int(nullable: false, identity: true),
                        LettresPerceptron = c.String(nullable: false, maxLength: 1),
                        BitArray = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.PerceptronID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Perceptron");
        }
    }
}
