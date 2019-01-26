namespace ProjectMangement.Entity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Project_ID = c.Int(nullable: false, identity: true),
                        Project = c.String(nullable: false, maxLength: 100),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Priority = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Project_ID)
                .ForeignKey("dbo.Users", t => t.User_ID, cascadeDelete: true)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        User_ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        EmployeeId = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.User_ID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Task_ID = c.Int(nullable: false, identity: true),
                        Parent_ID = c.Int(nullable: false),
                        Project_ID = c.Int(nullable: false),
                        User_ID = c.Int(nullable: false),
                        Task = c.String(nullable: false, maxLength: 150),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Priority = c.Int(nullable: false),
                        Status = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.Task_ID)
                .ForeignKey("dbo.Projects", t => t.Project_ID, cascadeDelete: true)
                .Index(t => t.Project_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "Project_ID", "dbo.Projects");
            DropForeignKey("dbo.Projects", "User_ID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "Project_ID" });
            DropIndex("dbo.Projects", new[] { "User_ID" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
        }
    }
}
