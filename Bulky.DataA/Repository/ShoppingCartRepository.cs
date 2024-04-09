using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System.Linq.Expressions;

namespace BulkyBook.DataA.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ShoppingCart obj)
        {
            _db.ShoppingCart.Update(obj);
        }
    }
}
