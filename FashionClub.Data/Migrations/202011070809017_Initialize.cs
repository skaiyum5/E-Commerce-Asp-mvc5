namespace FashionClub.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialize : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Featured = c.Boolean(nullable: false),
                        PictureID = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Summary = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountPercentage = c.Int(),
                        AfterDiscountPrice = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        CreteDateTime = c.DateTime(nullable: false),
                        UpdateDateTime = c.DateTime(),
                        Thumbnail = c.Int(nullable: false),
                        Featured = c.Boolean(nullable: false),
                        BrandID = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.ProductPictures",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RouteInfoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        ReturnType = c.String(),
                        Attributes = c.String(),
                        Area = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CategoryProducts",
                c => new
                    {
                        Category_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_ID, t.Product_ID })
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.Category_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.SubCategoryProducts",
                c => new
                    {
                        SubCategory_ID = c.Int(nullable: false),
                        Product_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SubCategory_ID, t.Product_ID })
                .ForeignKey("dbo.SubCategories", t => t.SubCategory_ID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ID, cascadeDelete: true)
                .Index(t => t.SubCategory_ID)
                .Index(t => t.Product_ID);
            
            CreateTable(
                "dbo.ProductSizes",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        SizeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.SizeID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.SizeID);
            
            CreateTable(
                "dbo.RouteInfoFashionClubRoles",
                c => new
                    {
                        RouteInfo_ID = c.Int(nullable: false),
                        FashionClubRole_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RouteInfo_ID, t.FashionClubRole_Id })
                .ForeignKey("dbo.RouteInfoes", t => t.RouteInfo_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.FashionClubRole_Id, cascadeDelete: true)
                .Index(t => t.RouteInfo_ID)
                .Index(t => t.FashionClubRole_Id);
            
            AddColumn("dbo.AspNetRoles", "Description", c => c.String());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "Address", c => c.String());
            AddColumn("dbo.AspNetUsers", "ZipCode", c => c.String());
            AddColumn("dbo.AspNetUsers", "ProfilePicture", c => c.Binary());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RouteInfoFashionClubRoles", "FashionClubRole_Id", "dbo.AspNetRoles");
            DropForeignKey("dbo.RouteInfoFashionClubRoles", "RouteInfo_ID", "dbo.RouteInfoes");
            DropForeignKey("dbo.ProductSizes", "SizeID", "dbo.Sizes");
            DropForeignKey("dbo.ProductSizes", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.SubCategoryProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.SubCategoryProducts", "SubCategory_ID", "dbo.SubCategories");
            DropForeignKey("dbo.CategoryProducts", "Product_ID", "dbo.Products");
            DropForeignKey("dbo.CategoryProducts", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Products", "BrandID", "dbo.Brands");
            DropForeignKey("dbo.Brands", "PictureID", "dbo.Pictures");
            DropIndex("dbo.RouteInfoFashionClubRoles", new[] { "FashionClubRole_Id" });
            DropIndex("dbo.RouteInfoFashionClubRoles", new[] { "RouteInfo_ID" });
            DropIndex("dbo.ProductSizes", new[] { "SizeID" });
            DropIndex("dbo.ProductSizes", new[] { "ProductID" });
            DropIndex("dbo.SubCategoryProducts", new[] { "Product_ID" });
            DropIndex("dbo.SubCategoryProducts", new[] { "SubCategory_ID" });
            DropIndex("dbo.CategoryProducts", new[] { "Product_ID" });
            DropIndex("dbo.CategoryProducts", new[] { "Category_ID" });
            DropIndex("dbo.ProductPictures", new[] { "PictureID" });
            DropIndex("dbo.ProductPictures", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "BrandID" });
            DropIndex("dbo.Brands", new[] { "PictureID" });
            DropColumn("dbo.AspNetUsers", "ProfilePicture");
            DropColumn("dbo.AspNetUsers", "ZipCode");
            DropColumn("dbo.AspNetUsers", "Address");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Country");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Description");
            DropTable("dbo.RouteInfoFashionClubRoles");
            DropTable("dbo.ProductSizes");
            DropTable("dbo.SubCategoryProducts");
            DropTable("dbo.CategoryProducts");
            DropTable("dbo.RouteInfoes");
            DropTable("dbo.Sizes");
            DropTable("dbo.ProductPictures");
            DropTable("dbo.Products");
            DropTable("dbo.Brands");
        }
    }
}
