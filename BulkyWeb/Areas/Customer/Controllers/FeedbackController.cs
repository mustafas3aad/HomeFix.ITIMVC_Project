using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class FeedbackController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private readonly UserManager<IdentityUser> _userManager;

        public FeedbackController(IUnitofWork unitofWork, UserManager<IdentityUser> userManager)
        {
            _unitofWork = unitofWork;
            _userManager = userManager;
        }

      
        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Index()
        {
            List<Feedback> feedbacks = _unitofWork.Feedback
                .GetAll(includeProperties: "Service")
                .OrderByDescending(f => f.CreatedAt)
                .ToList();

            return View(feedbacks);
        }
        [Authorize(Roles = SD.Role_Admin)]
        
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var feedback = _unitofWork.Feedback.Get(
                f => f.Id == id,
                includeProperties: "Service"
            );

            if (feedback == null)
            {
                return NotFound();
            }

            return View(feedback);
        }

       
        [Authorize]
        public IActionResult Create()
        {
            LoadServiceList();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]

        public async Task<IActionResult> Create([Bind("Comment,Rating,ServiceId")] Feedback obj)
        {
            
            ModelState.Remove("Service");
            ModelState.Remove("UserName");

            if (ModelState.IsValid)
            {
                
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    obj.UserName = user.UserName;
                }
                else
                {
                    
                    obj.UserName = "Anonymous";
                }

                
                obj.CreatedAt = System.DateTime.Now;

               
                _unitofWork.Feedback.Add(obj);
                _unitofWork.Save();

                TempData["success"] = "Feedback created successfully!";

                
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Details", new { id = obj.Id });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

           
            LoadServiceList();
            return View(obj);
        }

        private void LoadServiceList()
        {
            ViewBag.ServiceList = _unitofWork.Service.GetAll()
                .OrderBy(s => s.Name)
                .Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                })
                .ToList();
        }
    }
}