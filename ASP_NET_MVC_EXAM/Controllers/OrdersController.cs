using Core.Dtos;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using Data;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASP_NET_MVC_EXAM.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly CatalogDbContext context;
        private readonly ICartService cartService;
        private readonly IEmailSender emailSender;
        private readonly IViewRender viewRender;

        private string CurrentUserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        private string CurrentUserEmail => User.FindFirstValue(ClaimTypes.Email)!;

        public OrdersController(
            CatalogDbContext context, 
            ICartService cartService,
            IEmailSender emailSender,
            IViewRender viewRender)
        {
            this.context = context;
            this.cartService = cartService;
            this.emailSender = emailSender;
            this.viewRender = viewRender;
        }

        public IActionResult Index()
        {
            var orders = context.Orders
                                .Include(x => x.User)
                                .Where(x => x.UserId == CurrentUserId)
                                .ToList();
            return View(orders);
        }

        public async Task<IActionResult> Create()
        {
            var products = cartService.GetProductsEntity();

            var order = new Order()
            {
                CreatedAt = DateTime.Now,
                UserId = CurrentUserId,
                Mercedes = cartService.GetProductsEntity()
            };

            context.Orders.Add(order);
            context.SaveChanges();

            var totalPrice = products.Sum(x => x.Price);

            var html = viewRender.Render("MailTemplates/OrderSummary", new OrderSummaryModel
            {
                OrderNumber = order.Id,
                UserName = CurrentUserEmail,
                Mercedeses = cartService.GetProducts(),
                TotalPrice = totalPrice
            });

            // send email of order to the client
            await emailSender.SendEmailAsync(CurrentUserEmail, $"New Order #{order.Id}", html);

            cartService.Clear();

            return RedirectToAction("Index");
        }
    }
}
