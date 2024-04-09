using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface ICategoryRepository :  IRepository<Category>
    {
        void Update(Category obj);
    }
}
