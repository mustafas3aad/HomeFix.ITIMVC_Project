using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IReposaitory;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitofWork unitofWork;
        private readonly IEmailSender _emailSender;
 

        [BindProperty]
        public BookingCartVM BookigCartVM { get; set; }

        public CartController(IUnitofWork unitofWork, IEmailSender emailSender)
        {
            this.unitofWork = unitofWork;
            _emailSender = emailSender;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
        
            BookigCartVM = new()
            {
                BookingCartList = unitofWork.BookingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Team"),
                orderHeader=new()
                
            };


      
            IEnumerable<TeamImages> productImages = unitofWork.TeamImage.GetAll();

         
            foreach (var cart in BookigCartVM.BookingCartList)
            {
                
                cart.Team.TeamImages = productImages.Where(u => u.TeamId == cart.Team.Id).ToList();
                
                cart.Price = cart.Team.DepositAmount;
                BookigCartVM.orderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(BookigCartVM);
        }




        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

          
            BookigCartVM = new()
            {
                BookingCartList = unitofWork.BookingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Team").ToList(),
                orderHeader = new()
            };

            BookigCartVM.orderHeader.ApplicationUser = unitofWork.ApplicationUser.Get(u => u.Id == userId);

            BookigCartVM.orderHeader.Name = BookigCartVM.orderHeader.ApplicationUser.Name;
            BookigCartVM.orderHeader.PhoneNumber = BookigCartVM.orderHeader.ApplicationUser.PhoneNumber;
            BookigCartVM.orderHeader.StreetAddress = BookigCartVM.orderHeader.ApplicationUser.StreetAddress;
            BookigCartVM.orderHeader.City = BookigCartVM.orderHeader.ApplicationUser.City;
            BookigCartVM.orderHeader.State = BookigCartVM.orderHeader.ApplicationUser.State;

           
            BookigCartVM.orderHeader.OrderTotal = 0;

            foreach (var cart in BookigCartVM.BookingCartList)
            {
               
                cart.Price = cart.Team.DepositAmount;
               
                BookigCartVM.orderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            return View(BookigCartVM);
        }


      


        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            
            BookigCartVM.BookingCartList = unitofWork.BookingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "Team").ToList();

            BookigCartVM.orderHeader.OrderDate = System.DateTime.Now;
            BookigCartVM.orderHeader.ApplicationUserId = userId;

            ApplicationUser applicationUser = unitofWork.ApplicationUser.Get(u => u.Id == userId);

            
            BookigCartVM.orderHeader.OrderTotal = 0;

            foreach (var cart in BookigCartVM.BookingCartList)
            {
               
                if (cart.Count <= 0) cart.Count = 1;

                cart.Price = cart.Team.DepositAmount;
                BookigCartVM.orderHeader.OrderTotal += (cart.Price * cart.Count);
            }

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                BookigCartVM.orderHeader.PaymentStatus = SD.PaymentStatusPending;
                BookigCartVM.orderHeader.OrderStatus = SD.StatusPending;
            }
            else
            {
                BookigCartVM.orderHeader.PaymentStatus = SD.PaymentStatusDelayedPayment;
                BookigCartVM.orderHeader.OrderStatus = SD.StatusApproved;
            }

         
            unitofWork.OrderHeader.Add(BookigCartVM.orderHeader);
            unitofWork.Save();

            foreach (var cart in BookigCartVM.BookingCartList)
            {
                OrderDetail orderDetail = new()
                {
                    TeamId = cart.TeamId,
                    OrderHeaderId = BookigCartVM.orderHeader.Id,
                    Price = cart.Price,
                    Count = cart.Count 
                };
                unitofWork.OrderDetail.Add(orderDetail);
            }
          
            unitofWork.Save();

            if (applicationUser.CompanyId.GetValueOrDefault() == 0)
            {
                var domain = Request.Scheme + "://" + Request.Host.Value + "/";
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + $"customer/cart/OrderConfirmation?id={BookigCartVM.orderHeader.Id}",
                    CancelUrl = domain + "customer/cart/index",
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                };

                foreach (var item in BookigCartVM.BookingCartList)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                          
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Team.Title
                            }
                        },
                        Quantity = item.Count 
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);

                unitofWork.OrderHeader.UpdateStripPaymentID(BookigCartVM.orderHeader.Id, session.Id, session.PaymentIntentId);
                unitofWork.Save();

                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }

            return RedirectToAction(nameof(OrderConfirmation), new { id = BookigCartVM.orderHeader.Id });
        }


        public IActionResult OrderConfirmation(int id)
        {
			OrderHeader orderHeader = unitofWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            if (orderHeader.PaymentStatus != SD.PaymentStatusDelayedPayment)
            {
               
                var service = new SessionService();
                Session session = service.Get(orderHeader.SessionId);

                if (session.PaymentStatus.ToLower() == "paid")
                {
                    unitofWork.OrderHeader.UpdateStripPaymentID(id, session.Id, session.PaymentIntentId);
                    unitofWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    unitofWork.Save();
                }

                HttpContext.Session.Clear();
            }

           

            _emailSender.SendEmailAsync(orderHeader.ApplicationUser.Email,
                $"<p>New Order Created - {orderHeader.Id}</p>",
                $"Dear {orderHeader.ApplicationUser.Name}," +
                $"\r\n\r\nThank you for your payment!\r\n\r\nYour HomeFix team is now on the way to your location.\r\n\r\n" +
                $" If you need to cancel your booking, you can do so easily through the \"Manage Orders\" section on our website.\r\n\r\n" +
                $"We appreciate your trust in HomeFix.\r\n\r\nBest regards,\r\n" +
                $"HomeFix Team" );
              

            

            List<BookingCart> shoppingCarts = unitofWork.BookingCart
			 .GetAll(u => u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();

			unitofWork.BookingCart.RemoveRange(shoppingCarts);
			unitofWork.Save();
			return View(id);
        }



        public IActionResult Remove(int cartId)
        {
            var cartFromDb = unitofWork.BookingCart.Get(u => u.Id == cartId ,tracked:true);
            
            HttpContext.Session.SetInt32(SD.SessionCart, unitofWork.BookingCart
              .GetAll(u => u.ApplicationUserId == cartFromDb.ApplicationUserId).Count() - 1);

            unitofWork.BookingCart.Remove(cartFromDb);
            unitofWork.Save();
            return RedirectToAction(nameof(Index));

        }



    }
}
