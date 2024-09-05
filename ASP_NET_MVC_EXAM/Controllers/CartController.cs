using AutoMapper;
using Core.Dtos;
using Core.Services;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Extensions;
using ShopMvcApp_PD211.Services;

namespace ShopMvcApp_PD211.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService)
        {
            this.cartService = cartService;
        }
        // відображає сторінку корзини із доданими продуктами
        public IActionResult Index()
        {
            return View(cartService.GetProducts());
        }

        // додає продукт в корзину
        public IActionResult Add(int id, string? returnUrl)
        {
            cartService.Add(id);
            return Redirect(returnUrl ?? "/");
        }

        // видаляє продукт з корзини
        public IActionResult Remove(int id)
        {
            cartService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}