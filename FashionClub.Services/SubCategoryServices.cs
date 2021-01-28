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
    public class SubCategoryServices
    {
        #region singleTon
        public static SubCategoryServices Instance
        {
            get
            {
                if (instance == null) instance = new SubCategoryServices();
                {
                    return instance;
                }

            }
        }
        private static SubCategoryServices instance { get; set; }
        public SubCategoryServices()
        {

        }
        #endregion

        public List<SubCategory> SearchSubCategories(string searchTerm,int? categoryId,int pageNo,int pageSize)
        {
            FashionClubDBContext context = new FashionClubDBContext();
            var subCategory = context.SubCategories.Include(x=>x.Categories).ToList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                subCategory = subCategory.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Description.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            if (categoryId.HasValue && categoryId>0)
            {
                subCategory = subCategory.Where(x => x.Categories.Any(y=>y.ID.Equals(categoryId))).ToList();
            }
            return subCategory.OrderBy(x=>x.ID).Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
        }
        public int SearchSubCategoriesCount(string searchTerm,int? categoryId)
        {
            FashionClubDBContext context = new FashionClubDBContext();
            var subCategory = context.SubCategories.Include(x=>x.Categories).ToList();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                subCategory = subCategory.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Description.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            if (categoryId.HasValue && categoryId > 0)
            {
                subCategory = subCategory.Where(x => x.Categories.Any(y=>y.ID.Equals(categoryId))).ToList();
            }
            return subCategory.Count();
        }

        public List<SubCategory> GetAll()
        {
            FashionClubDBContext context = new FashionClubDBContext();
            return context.SubCategories.ToList();
        }
        public SubCategory GetByID(int ID)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var subCategory = context.SubCategories.Include(x => x.Categories).Where(y => y.ID == ID).FirstOrDefault();
                return subCategory;
                
            }
        }
        public IQueryable<SubCategory> GetByCategoryIds(List<int> Ids)
        {
            FashionClubDBContext context = new FashionClubDBContext();
            var subCategories = context.SubCategories.Where(x => x.Categories.Any(y => Ids.Contains(y.ID))).AsQueryable();
            return subCategories;
           
        }

        public void Create(SubCategory subCategory, List<int> IDs)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var categories = context.Categories.ToList();
                var subC = new SubCategory();
                subC = context.SubCategories.Add(subCategory);
                subC.Categories.AddRange(categories.Where(x => IDs.Contains(x.ID)));


                //var categories = new List<Category>();
                //foreach (var id in IDs)
                //{
                //    categories.Add(context.Categories.Attach(new Category { ID = id }));

                //}
                //var subC = context.SubCategories.Add(subCategory);
                //subC.Categories = categories;

                context.SaveChanges();
            }
            
        }
        public bool Update(SubCategory subCategory, List<int> Ids)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Entry(subCategory).State=EntityState.Modified ;
                context.Entry(subCategory).Collection(x => x.Categories).Load();
                var categories = context.Categories.Where(x => Ids.Contains(x.ID)).ToList();
                subCategory.Categories = categories;
                return context.SaveChanges() > 0;
            }
        }
        public bool Delete(int ID)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var subCategory = context.SubCategories.Find(ID);
                context.Entry(subCategory).State = System.Data.Entity.EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }

    }
}
