using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using Bulky.Models.View_Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class TeamController : Controller
    {
        private readonly IUnitofWork unitofWork;

        public IWebHostEnvironment WebHostEnvironment { get; }
                                                       
        public TeamController(IUnitofWork unitofWork,IWebHostEnvironment webHostEnvironment)
        {
            this.unitofWork = unitofWork;
            WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Team> Workers = unitofWork.Team.GetAll(includeProperties: "Service").ToList();
            return View(Workers);
        }

     
        public IActionResult Upsert(int? id)
        {
            
             
            TeamVM serviceVM = new()
            {
              
                ServiceList = unitofWork.Service.GetAll()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),

                Team = new Team()
            };

            

            if (id == null || id == 0)
            {
                
                return View(serviceVM);
            }
            else
            {                                           
                serviceVM.Team = unitofWork.Team.Get(u=>u.Id == id,includeProperties:"TeamImages");
                return View(serviceVM);
            }
  
        }
        [HttpPost]
        public IActionResult  Upsert(TeamVM workerVM, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
            

                if (workerVM.Team.Id == 0)
                {
                    unitofWork.Team.Add(workerVM.Team);
                }
                else
                {
                    unitofWork.Team.update(workerVM.Team);
                }
                unitofWork.Save();


                string wwwRootPath = WebHostEnvironment.WebRootPath;
                if (files != null)
                {

                    foreach (IFormFile file in files)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        string WorkerPath = @"images\teams\team-" + workerVM.Team.Id;
                        string finalPath = Path.Combine(wwwRootPath, WorkerPath);

                        if (!Directory.Exists(finalPath))
                            Directory.CreateDirectory(finalPath);

                        using (var fileStream = new FileStream(Path.Combine(finalPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                        TeamImages WorkerImage = new()
                        {
                            ImageUrl = @"\" + WorkerPath + @"\" + fileName,
                            TeamId = workerVM.Team.Id,
                        };

                        if (workerVM.Team.TeamImages == null)
                            workerVM.Team.TeamImages = new List<TeamImages>();

                        workerVM.Team.TeamImages.Add(WorkerImage);

                    }
                   

                    unitofWork.Team.update(workerVM.Team);
                    unitofWork.Save();




                }



                TempData["success"] = "Worker created/updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {

                workerVM.ServiceList = unitofWork.Service.GetAll().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });

                return View(workerVM);
            }
        }

      

        public IActionResult DeleteImage(int imageId)
        {
            var imageToBeDeleted = unitofWork.TeamImage.Get(u => u.Id == imageId);
            int workerId = imageToBeDeleted.TeamId;
            if (imageToBeDeleted != null)
            {
                if (!string.IsNullOrEmpty(imageToBeDeleted.ImageUrl))
                {
                    var oldImagePath =
                                   Path.Combine(WebHostEnvironment.WebRootPath,
                                   imageToBeDeleted.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                unitofWork.TeamImage.Remove(imageToBeDeleted);
                unitofWork.Save();

                TempData["success"] = "Deleted successfully";
            }

            return RedirectToAction(nameof(Upsert), new { id = workerId });
        }


        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Team> objProductList = unitofWork.Team.GetAll(includeProperties: "Service").ToList();
            return Json(new { data = objProductList });
        }


        
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var workerToBeDeleted =unitofWork.Team.Get(u=>u.Id == id);
            if(workerToBeDeleted == null)
            {
                return Json(new {success = false,message = "Error while deleting"});
            }


           
            string workerPath = @"images\teams\team-" + id;
            string finalPath = Path.Combine(WebHostEnvironment.WebRootPath, workerPath);

            if (Directory.Exists(finalPath))
            {
                string[] filePaths = Directory.GetFiles(finalPath);
                foreach (string filePath in filePaths)
                {
                    System.IO.File.Delete(filePath);
                }

                Directory.Delete(finalPath);
            }




       

            unitofWork.Team.Remove(workerToBeDeleted);
            unitofWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }

}
