using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface ICompanyRepository :  IRepository<Company>
    {
        void Update(Company obj);
    }
}
