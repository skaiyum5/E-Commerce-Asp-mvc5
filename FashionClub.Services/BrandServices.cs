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
    public class BrandServices
    {
        #region singleTon
        public static BrandServices Instance
        {
            get
            {
                if (instance == null) instance = new BrandServices();
                {
                    return instance;
                }

            }
        }
        private static BrandServices instance { get; set; }
        public BrandServices()
        {

        }
        #endregion 

        public List<Brand> SearchBrands(string searchTerm,int pageNo,int itemPerPage)
        {
            FashionClubDBContext context = new FashionClubDBContext();

            var brands = context.Brands.Include(x => x.Picture).Include(x => x.Products).ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                brands = brands.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            return brands.OrderByDescending(x => x.ID).Skip((pageNo - 1) * itemPerPage).Take(itemPerPage).ToList();
              
        }
        public int SearchBrandsCount(string searchTerm, int pageNo, int itemPerPage)
        {
            FashionClubDBContext context = new FashionClubDBContext();

            var brands = context.Brands.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                brands = brands.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            return brands.Count();

        }
        public List<Brand> GetBrands()
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
              return context.Brands.Include("Picture").Include("Products").ToList();
            }
              
        }
        public Brand GetByID(int Id)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
              return context.Brands.Include("Picture").Include("Products").Where(x=>x.ID==Id).FirstOrDefault();
            }
              
        }
        public bool Create(Brand brand)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Brands.Add(brand);
                return context.SaveChanges()>0;
            }
        }
        public bool Update(Brand brand)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Entry(brand).State=System.Data.Entity.EntityState.Modified;
                return context.SaveChanges()>0;
            }
        }
        public bool Delete(int? Id)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var brand = context.Brands.Find(Id.Value);
                context.Entry(brand).State=System.Data.Entity.EntityState.Deleted;
                return context.SaveChanges()>0;
            }
        }
    }
}
