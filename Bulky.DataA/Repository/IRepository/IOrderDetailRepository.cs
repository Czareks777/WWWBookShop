using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface IOrderDetailRepository :  IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
    }
}
