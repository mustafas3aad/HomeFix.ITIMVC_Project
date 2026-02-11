using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IUnitofWork unitofWork { get; }

        public HomeController(ILogger<HomeController> logger,IUnitofWork unitofWork)
        {
            _logger = logger;
             this.unitofWork = unitofWork;
        }



        public IActionResult Index()
        {





            IEnumerable<Team> productList = unitofWork.Team.GetAll(includeProperties: "Service,TeamImages");
            return View(productList);
        }

        public IActionResult Details(int ProductId)
        {
        
          
            BookingCart cart = new()
            {                                                                                          
                Team = unitofWork.Team.Get(u => u.Id == ProductId, includeProperties: "Service,TeamImages"),
                Count = 1,
                TeamId = ProductId
            };
           
            return View(cart);
        }

        [HttpPost]
      
        [Authorize]
        public IActionResult Details(BookingCart ShoppingCart)
        {
           
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
          
            ShoppingCart.ApplicationUserId = userId;



            BookingCart CartFromDB = unitofWork.BookingCart.Get(u => u.ApplicationUserId == userId &&
            u.TeamId == ShoppingCart.TeamId);
            if(CartFromDB != null)
            {
                
                CartFromDB.Count += ShoppingCart.Count;


                unitofWork.BookingCart.update(CartFromDB);
            }
            else
            {
                unitofWork.BookingCart.Add(ShoppingCart);
                unitofWork.Save();
              
                HttpContext.Session.SetInt32(SD.SessionCart,

                    unitofWork.BookingCart.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            
           

            TempData["Success"] = "Cart Updated Successfully";
            
            return RedirectToAction(nameof(Index));
        }

        


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
