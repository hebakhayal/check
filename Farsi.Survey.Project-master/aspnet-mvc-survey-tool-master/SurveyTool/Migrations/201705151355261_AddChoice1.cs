namespace SurveyTool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddChoice1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserSurveys", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserSurveys", "Survey_Id", "dbo.Surveys");
            DropForeignKey("dbo.Responses", "FK_UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Responses", new[] { "FK_UserId" });
            DropIndex("dbo.ApplicationUserSurveys", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserSurveys", new[] { "Survey_Id" });
            CreateTable(
                "dbo.Choices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        Body = c.String(),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.QuestionId)
                .Index(t => t.Question_Id);
            
            AddColumn("dbo.Questions", "DefaultValue", c => c.String());
            AddColumn("dbo.Questions", "Validation_Question", c => c.String());
            AddColumn("dbo.Surveys", "Summary", c => c.String());
            AddColumn("dbo.Surveys", "Thumbnail", c => c.Binary());
            DropColumn("dbo.Responses", "FK_UserId");
            DropTable("dbo.ApplicationUserSurveys");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserSurveys",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Survey_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Survey_Id });
            
            AddColumn("dbo.Responses", "FK_UserId", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Choices", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Choices", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Choices", new[] { "Question_Id" });
            DropIndex("dbo.Choices", new[] { "QuestionId" });
            DropColumn("dbo.Surveys", "Thumbnail");
            DropColumn("dbo.Surveys", "Summary");
            DropColumn("dbo.Questions", "Validation_Question");
            DropColumn("dbo.Questions", "DefaultValue");
            DropTable("dbo.Choices");
            CreateIndex("dbo.ApplicationUserSurveys", "Survey_Id");
            CreateIndex("dbo.ApplicationUserSurveys", "ApplicationUser_Id");
            CreateIndex("dbo.Responses", "FK_UserId");
            AddForeignKey("dbo.Responses", "FK_UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ApplicationUserSurveys", "Survey_Id", "dbo.Surveys", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserSurveys", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
