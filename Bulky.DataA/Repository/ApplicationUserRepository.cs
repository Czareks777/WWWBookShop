using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using System.Linq.Expressions;

namespace BulkyBook.DataA.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(ApplicationUser obj)
        {
            _db.ApplicationUsers.Update(obj);
        }
    }
}
