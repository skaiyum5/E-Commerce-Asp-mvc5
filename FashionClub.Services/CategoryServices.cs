using FashionClub.Data;
using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Services
{
    public class CategoryServices
    {
        #region singleTon
        public static CategoryServices Instance
        {
            get
            {
                if (instance == null) instance = new CategoryServices();
                {
                    return instance;
                }

            }
        }
        private static CategoryServices instance { get; set; }
        public CategoryServices()
        {

        }
        #endregion 

        public List<Category> GetAll()
        {
            FashionClubDBContext context = new FashionClubDBContext();
            return context.Categories.OrderBy(x=>x.ID).ToList();
        }
        public IQueryable<Category> Categories()
        {
            FashionClubDBContext context = new FashionClubDBContext();
            return context.Categories.OrderBy(x=>x.ID).AsQueryable();
        }
        public Category GetByID(int ID)
        {
            FashionClubDBContext context = new FashionClubDBContext();
              var category = context.Categories.Find(ID);
              return category;
            
        }
        public Category GetCategpryWithSubCategoryByIds(int ID)
        {

            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var CategoriesWithSubCategory = context.Categories.Include(x=>x.SubCategories).Where(y => y.ID == ID).FirstOrDefault();
                return CategoriesWithSubCategory;
            }

        }
        public bool Create(Category category)
        {
            using(FashionClubDBContext context=new FashionClubDBContext())
            {
                context.Categories.Add(category);
                return context.SaveChanges()>0;
            }
        }

        public bool Update(Category category)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                return context.SaveChanges()>0;
            }
        }
        public bool Delete(int ID)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var category = context.Categories.Find(ID);
                context.Entry(category).State = System.Data.Entity.EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }
    }
}
