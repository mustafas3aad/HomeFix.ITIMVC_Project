using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    

    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class ServiceController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public ServiceController(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }
        public IActionResult Index()
        {
            List<Service> services = unitofWork.Service.GetAll().ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service obj)
        {
        
            if (ModelState.IsValid)
            {
                unitofWork.Service.Add(obj);
                unitofWork.Save();

              

                TempData["Success"] = "Service Created Sucessfully";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Service? ServicesFromDb = unitofWork.Service.Get(x => x.Id == id);
          
            if (ServicesFromDb == null)
                return NotFound();

            return View(ServicesFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Service obj)
        {
            if (ModelState.IsValid)
            {
                unitofWork.Service.update(obj);
                unitofWork.Save();
                TempData["Success"] = "Service Update Sucessfully";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            
            if (id == null || id == 0)
                return NotFound();

            Service? ServiceFromDb = unitofWork.Service.Get(x => x.Id == id);

            
            if (ServiceFromDb == null)
                return NotFound();

            return View(ServiceFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Service? ServiceFromDb = unitofWork.Service.Get(x => x.Id == id);

            
            if (ServiceFromDb == null)
                return NotFound();

            unitofWork.Service.Remove(ServiceFromDb);
            unitofWork.Save();
            TempData["Success"] = "Service Deleted Sucessfully";
            return RedirectToAction(nameof(Index));


        }
    }
}
