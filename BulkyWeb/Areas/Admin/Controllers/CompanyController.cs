using BulkyBook.DataA;
using BulkyBook.DataA.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using Elfie.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }
        public IActionResult Upsert(int? id)
        {
          
           
            if (id == null || id == 0)
            {
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {

            if (ModelState.IsValid)
            {
                
                
                if (companyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(companyObj);
                    
                }
                else
                {
                    _unitOfWork.Company.Update(companyObj);
                    
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {

                {
                    return View(companyObj);

                };
               
            }

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
           
            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete succesful" });
        }
        #endregion
    }
}

