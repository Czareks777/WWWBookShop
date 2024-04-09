using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface IProductRepository :  IRepository<Product>
    {
        void Update(Product obj);
    }
}
