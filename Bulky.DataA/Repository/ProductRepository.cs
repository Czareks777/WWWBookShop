using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System.Linq.Expressions;

namespace BulkyBook.DataA.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

       
        public void Update(Product obj)
        {
           var objFromDb=_db.Products.FirstOrDefault(u=>u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title=obj.Title;//
                objFromDb.Description=obj.Description;//
                objFromDb.Author=obj.Author;//
                objFromDb.CategoryId=obj.CategoryId;//
                objFromDb.ISBN=obj.ISBN;//
                objFromDb.Price100=obj.Price100;//
                objFromDb.Price50 = obj.Price50;//
                objFromDb.Price=obj.Price;//
                objFromDb.ListPrice=obj.ListPrice;//
                if(obj.ImageUrl!=null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }


            }
        }
    }
}
