namespace MillionaireGame.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Correct = c.Boolean(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionStatistics",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        QuestionCounter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.AnswerStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerCounter = c.Int(nullable: false),
                        QuestionStatisticId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuestionStatistics", t => t.QuestionStatisticId, cascadeDelete: true)
                .Index(t => t.QuestionStatisticId);
            
            CreateTable(
                "dbo.ExceptionDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ActionName = c.String(),
                        StackTrace = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionStatistics", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.AnswerStatistics", "QuestionStatisticId", "dbo.QuestionStatistics");
            DropIndex("dbo.AnswerStatistics", new[] { "QuestionStatisticId" });
            DropIndex("dbo.QuestionStatistics", new[] { "QuestionId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.ExceptionDetails");
            DropTable("dbo.AnswerStatistics");
            DropTable("dbo.QuestionStatistics");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
