using FashionClub.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Data
{
    public class FashionClubDBContext : IdentityDbContext<FashionClubUser>
    {
        public FashionClubDBContext() : base("name=FashionClubConnection")
        {
            //InitializeDatabase();
            Database.SetInitializer<FashionClubDBContext>(new FashionClubInitializer());
        }

        //protected virtual void InitializeDatabase()
        //{
        //    Database.SetInitializer(new FashionClubInitializer());
        //    if (! Database.Exists())
        //    {
        //        Database.Initialize(true);
        //    };
        //}

        public static FashionClubDBContext Create()
        {
            return new FashionClubDBContext();
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductPicture> ProductPictures { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<RouteInfo> RouteInfos { get; set; }
        //public DbSet<FashionClubRoleRoutes> FashionClubRoleRoutes { get; set; }
        public DbSet<FashionClubRole> FashionClubRoles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany<SubCategory>(s => s.SubCategories)
                .WithMany(c => c.Categories)
                .Map(cs =>
                {
                    cs.MapLeftKey("CategoryID");
                    cs.MapRightKey("SubCategoryID");
                    cs.ToTable("CategorySubCategory");
                });
            //modelBuilder.Entity<FashionClubRole>()
                //.HasMany(x=>x.RouteInfos)
                //.WithOptional(x=>x.FashionClubRole)
                //.Map(cs =>
                //{
                //    cs.MapKey("FashionClubRole_Id");
                //    cs.MapKey("RouteInfoID");
                //    cs.ToTable("FashionClubRoleRoutes");
                //});

            modelBuilder.Entity<Product>()
                .HasMany<Size>(s => s.Sizes)
                .WithMany(c => c.Products)
                .Map(cs =>
                {
                    cs.MapLeftKey("ProductID");
                    cs.MapRightKey("SizeID");
                    cs.ToTable("ProductSizes");
                });
            //modelBuilder.Entity<Category>().ToTable("Category");
            //modelBuilder.Entity<SubCategory>().ToTable("SubCategory");
        }

    }
}

