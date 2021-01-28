using FashionClub.Data;
using FashionClub.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FashionClub.Services
{
    public class ProductServices
    {
        #region singleTon

        public static ProductServices Instance
        {
            get
            {
                if (instance == null) instance = new ProductServices();
                {
                    return instance;
                }

            }
        }

        private static ProductServices instance { get; set; }

        public ProductServices()
        {

        }

        public List<Product> GetByIds(List<int> cardProductIds)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                return context.Products.Where(x => cardProductIds.Contains(x.ID)).ToList();
            }
        }

        #endregion

        public bool Create(Product product, List<int> categoryIds, List<int> subCategoryIds, List<int> sizeIDs)
        {
            FashionClubDBContext context = new FashionClubDBContext();

            var aProduct = context.Products.Add(product);
            aProduct.Categories = context.Categories.Where(x => categoryIds.Contains(x.ID)).ToList();
            aProduct.SubCategories = context.SubCategories.Where(x => subCategoryIds.Contains(x.ID)).ToList();
            aProduct.Sizes = context.Sizes.Where(x => sizeIDs.Contains(x.ID)).ToList();
            return context.SaveChanges() > 0;


            //var sizes = SizeServices.Instance.GetAll();
            //sizes = sizes.Where(x => sizeIDs.Contains(x.ID)).ToList();
            ////var sizes = new List<Size>();
            ////foreach (var iD in sizeIDs)
            ////{
            ////    sizes.Add(context.Sizes.Attach(new Size { ID = iD }));
            ////}
            //aProduct.Sizes = sizes;
        }

        public List<Product> Products(string SearchTerm, int? CategoryId, int? SubCategoryId, int pageSize, int PageNO)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var products = context.Products.Include(x => x.Categories).Include("SubCategories").AsQueryable();
                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    products = products.Where(x => x.Name.Contains(SearchTerm)||x.Summary.Contains(SearchTerm));
                }
                if (CategoryId.HasValue && CategoryId.Value > 0)
                {
                    products = products.Where(x => x.Categories.Any(y => y.ID == CategoryId)) ;
                }
                if (SubCategoryId.HasValue && SubCategoryId.Value > 0)
                {
                    products = products.Where(x => x.SubCategories.Any(y => y.ID == SubCategoryId)) ;
                }
                return products.OrderBy(x => x.ID).Skip((1 - PageNO) * pageSize).Take(pageSize).ToList();
            }
        }
        public IQueryable<Product> Products()
        {
            FashionClubDBContext context = new FashionClubDBContext();
                
            return context.Products.Include(i=>i.SubCategories).Include(x => x.Categories).Include(y=>y.ProductPictures).AsQueryable();

        }


        public bool Update(Product product, int[] categoryIds, [Optional] int[] subCategoryIds, int[] sizeIds)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                //var aProduct = context.Products.Include(x=>x.Sizes).FirstOrDefault(x=>x.ID== product.ID);
                //context.ProductPictures.RemoveRange(aProduct.ProductPictures);
                //context.ProductPictures.AddRange(product.ProductPictures);

                context.ProductPictures.RemoveRange(context.ProductPictures.Where(x=>x.ProductID==product.ID));
                context.ProductPictures.AddRange(product.ProductPictures);
                product.ProductPictures.Clear();

                //context.Entry(product).State = EntityState.Detached;
                //context.Entry(aProduct).CurrentValues.SetValues(product);
                
                context.Entry(product).State = EntityState.Modified;
                
                context.Entry(product).Collection(x => x.Categories).Load();
                context.Entry(product).Collection(x => x.SubCategories).Load();
                context.Entry(product).Collection(x => x.Sizes).Load();
                                
                if (categoryIds != null && categoryIds.Count() > 0)
                {
                    product.Categories = context.Categories.Where(x => categoryIds.Contains(x.ID)).ToList();
                }
                else
                {
                    product.Categories = new List<Category>();
                }

                if (subCategoryIds != null && subCategoryIds.Count() > 0)
                {
                    product.SubCategories = context.SubCategories.Where(x => subCategoryIds.Contains(x.ID)).ToList();
                }
                else
                {
                    product.SubCategories = new List<SubCategory>();
                }
                if (sizeIds != null && sizeIds.Count() > 0)
                {
                    product.Sizes = context.Sizes.Where(x => sizeIds.Contains(x.ID)).ToList();
                }
                else
                {
                    product.Sizes = new List<Size>();
                }
                
                return context.SaveChanges() > 0;
            }
        }
        public bool Delete(int Id)
        {
            using (FashionClubDBContext context = new FashionClubDBContext())
            {
                var product = context.Products.Find(Id);
                context.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                return context.SaveChanges() > 0;
            }
        }
        public Product GetById(int Id)
        {
            FashionClubDBContext context = new FashionClubDBContext();

            var product = context.Products.Include(p => p.Categories).Include(p => p.SubCategories).Include(p => p.Brand).Include(p => p.Sizes).Include("ProductPictures").Where(x => x.ID == Id).FirstOrDefault();

            return product;
            //return context.Products.Find(Id);

        }
    }
}
