using BulkyBook.DataA;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class UserContorller : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserContorller(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult RoleManagment(string userId)
        {
            string RoleId=_db.UserRoles.FirstOrDefault(u=>u.UserId==userId).RoleId;

            RoleManagmentVM RoleVM = new RoleManagmentVM()
            {
                ApplicationUser = _db.ApplicationUsers.Include(u => u.Company).FirstOrDefault(u => u.Id == userId),
                RoleList = _db.Roles.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _db.Companies.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };

            RoleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u => u.Id == RoleId).Name;

            return View(RoleVM);
        }

        public IActionResult RoleManagment(RoleManagmentVM roleManagmentVM)
        {
            string RoleId = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagmentVM.ApplicationUser.Id).RoleId;
            string oldRole = _db.Roles.FirstOrDefault(u => u.Id == RoleId).Name;

            if (!(roleManagmentVM.ApplicationUser.Role == oldRole))
            {
                //role was updated
                ApplicationUser applicationUser = _db.ApplicationUsers.
                    FirstOrDefault(u => u.Id == roleManagmentVM.ApplicationUser.Id);
                if (roleManagmentVM.ApplicationUser.Role == StaticDetails.Role_Company)
                {
                    applicationUser.CompanyId=roleManagmentVM.ApplicationUser.CompanyId;
                }
                if(oldRole==StaticDetails.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _db.SaveChanges();  
                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagmentVM.ApplicationUser.Role).GetAwaiter().GetResult();

            }
            return RedirectToAction("Index");
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserList = _db.ApplicationUsers.Include(u=>u.Company).ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            
            foreach(var user in objUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleId).Name;
                if (user.Company == null)
                {
                    user.Company = new() { Name = "" };
                }

            }
            
            return Json(new { data = objUserList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        { 
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }
            
            if(objFromDb.LockoutEnd!=null&& objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _db.SaveChanges();

            return Json(new { success = true, message = "Operation succesful" });
        }
        #endregion
    }
}

