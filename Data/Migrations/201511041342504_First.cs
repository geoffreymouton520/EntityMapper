namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Domain",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.System",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DomainId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Domain", t => t.DomainId, cascadeDelete: true)
                .Index(t => t.DomainId);
            
            CreateTable(
                "dbo.Entity",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.System", t => t.SystemId, cascadeDelete: true)
                .Index(t => t.SystemId);
            
            CreateTable(
                "dbo.EntityMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        Confirmed = c.Boolean(nullable: false),
                        Correct = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ReviewedBy = c.String(),
                        ReviewedOn = c.DateTime(),
                        MappingOriginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entity", t => t.DestinationId)
                .ForeignKey("dbo.MappingOrigin", t => t.MappingOriginId, cascadeDelete: true)
                .ForeignKey("dbo.Entity", t => t.SourceId)
                .Index(t => t.SourceId)
                .Index(t => t.DestinationId)
                .Index(t => t.MappingOriginId);
            
            CreateTable(
                "dbo.MappingOrigin",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PropertyMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Confirmed = c.Boolean(nullable: false),
                        Correct = c.Boolean(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ReviewedBy = c.String(),
                        ReviewedOn = c.DateTime(),
                        SourceId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        MappingOriginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Property", t => t.DestinationId)
                .ForeignKey("dbo.MappingOrigin", t => t.MappingOriginId, cascadeDelete: true)
                .ForeignKey("dbo.Property", t => t.SourceId)
                .Index(t => t.SourceId)
                .Index(t => t.DestinationId)
                .Index(t => t.MappingOriginId);
            
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedOn = c.DateTime(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entity", t => t.EntityId, cascadeDelete: true)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.TestData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Source = c.String(),
                        Destination = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entity", "SystemId", "dbo.System");
            DropForeignKey("dbo.EntityMapping", "SourceId", "dbo.Entity");
            DropForeignKey("dbo.PropertyMapping", "SourceId", "dbo.Property");
            DropForeignKey("dbo.PropertyMapping", "MappingOriginId", "dbo.MappingOrigin");
            DropForeignKey("dbo.PropertyMapping", "DestinationId", "dbo.Property");
            DropForeignKey("dbo.Property", "EntityId", "dbo.Entity");
            DropForeignKey("dbo.EntityMapping", "MappingOriginId", "dbo.MappingOrigin");
            DropForeignKey("dbo.EntityMapping", "DestinationId", "dbo.Entity");
            DropForeignKey("dbo.System", "DomainId", "dbo.Domain");
            DropIndex("dbo.Property", new[] { "EntityId" });
            DropIndex("dbo.PropertyMapping", new[] { "MappingOriginId" });
            DropIndex("dbo.PropertyMapping", new[] { "DestinationId" });
            DropIndex("dbo.PropertyMapping", new[] { "SourceId" });
            DropIndex("dbo.EntityMapping", new[] { "MappingOriginId" });
            DropIndex("dbo.EntityMapping", new[] { "DestinationId" });
            DropIndex("dbo.EntityMapping", new[] { "SourceId" });
            DropIndex("dbo.Entity", new[] { "SystemId" });
            DropIndex("dbo.System", new[] { "DomainId" });
            DropTable("dbo.TestData");
            DropTable("dbo.Property");
            DropTable("dbo.PropertyMapping");
            DropTable("dbo.MappingOrigin");
            DropTable("dbo.EntityMapping");
            DropTable("dbo.Entity");
            DropTable("dbo.System");
            DropTable("dbo.Domain");
        }
    }
}
