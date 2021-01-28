using FashionClub.Data;
using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Services
{
    public class SizeServices
    {
        #region singleTon
        public static SizeServices Instance
        {
            get
            {
                if (instance == null) instance = new SizeServices();
                {
                    return instance;
                }

            }
        }
        private static SizeServices instance { get; set; }
        public SizeServices()
        {

        }
        #endregion 

        public List<Size> GetAll()
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                return context.Sizes.ToList();
            }

        }
        public List<Size> SearchSizes(string searchTerm,int pageNO,int pageSize)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var sizes = context.Sizes.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    return sizes.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).OrderBy(x=>x.ID).Skip((pageNO-1)*pageSize).Take(pageSize).ToList();
                }
                return sizes.OrderBy(x=>x.ID).Skip((pageNO - 1) * pageSize).Take(pageSize).ToList();
            }

        }
        public int SearchSizesCount(string searchTerm,int pageNO,int pageSize)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var sizes = context.Sizes.AsQueryable();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    return sizes.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList().Count();
                }
                return sizes.ToList().Count();
            }

        }
        public Size GetByID(int ID)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                return context.Sizes.Find(ID);
            }

        }
        public bool Create(Size size)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Sizes.Add(size);
                return context.SaveChanges() > 0;
            }
        }
        public bool Update(Size size)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                context.Entry(size).State=System.Data.Entity.EntityState.Modified;
                return context.SaveChanges() > 0;
            }
        }
        public bool Delete(int Id)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var size = context.Sizes.Find(Id);
                //context.Entry(size).State = System.Data.Entity.EntityState.Deleted;
                context.Sizes.Remove(size);
                return context.SaveChanges() > 0;
            }
        }
    }
}
