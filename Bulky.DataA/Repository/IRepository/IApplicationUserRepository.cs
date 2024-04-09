using BulkyBook.Models;

namespace BulkyBook.DataA.Repository.IRepository
{
    public interface IApplicationUserRepository :  IRepository<ApplicationUser>
    {
        void Update(ApplicationUser obj);
    }
}
