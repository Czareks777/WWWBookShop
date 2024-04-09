using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System.Linq.Expressions;

namespace BulkyBook.DataA.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetail.Update(obj);
        }
    }
}
