namespace SurveyTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);

            AddColumn("dbo.Questions", "Validation_Question", c => c.String());
            AddColumn("dbo.Questions", "DefaultValue", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropColumn("dbo.Questions", "Validation_Question");
            DropColumn("dbo.Questions", "DefaultValue");
            DropTable("dbo.Choices");
        }
    }
}
