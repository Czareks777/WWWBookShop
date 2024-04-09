using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface IShoppingCartRepository :  IRepository<ShoppingCart>
    {
        void Update(ShoppingCart obj);
    }
}
