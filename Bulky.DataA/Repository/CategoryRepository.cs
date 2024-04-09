using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System.Linq.Expressions;

namespace BulkyBook.DataA.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
