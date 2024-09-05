using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ShopMvcApp_PD211.Extensions;
using _03_SecondHomeWorkViewModel.Entities;

namespace ShopMvcApp_PD211.Controllers
{
    public class MercedesController : Controller
    {
        private readonly IMapper mapper;
        private readonly CatalogDbContext context;
        private readonly IFileService fileService;

        public MercedesController(IMapper mapper, CatalogDbContext context, IFileService fileService)
        {
            this.mapper = mapper;
            this.context = context;
            this.fileService = fileService;
        }

        public IActionResult Catalog()
        {
            // ... load data from database ...
            var mercedes = context.Mercedeses
                .Include(x => x.BrandOfCar) // LEFT JOIN
                .ToList();
            return View(mapper.Map<List<MercedesDto>>(mercedes));
        }

        public IActionResult Details(int id)
        {
            var product = context.Mercedeses.Find(id);

            if (product == null) return NotFound();

            return View(mapper.Map<MercedesDto>(product));
        }

        // GET - open create page
        [HttpGet]
        public IActionResult Create()
        {
            LoadBrands();
            ViewBag.CreateMode = true;
            return View("Upsert");
        }

        // POST - create object in db
        [HttpPost]
        public async Task<IActionResult> Create(MercedesDto model2)
        {
            if (!ModelState.IsValid)
            {
                LoadBrands();
                ViewBag.CreateMode = true;
                return View("Upsert", model2);
            }

            // 1 - manual mapping
            //var entity = new Product
            //{
            //    Title = model2.Title,
            //    CategoryId = model2.CategoryId,
            //    Description = model2.Description,
            //    Discount = model2.Discount,
            //    ImageUrl = model2.ImageUrl,
            //    Price = model2.Price,
            //    Quantity = model2.Quantity
            //};
            // 2 - using auto mapper

            // save image and get file path
            if (model2.Image != null)
                model2.ImgUrl = await fileService.SaveProductImage(model2.Image);

            var entity = mapper.Map<Mercedes>(model2);

            context.Mercedeses.Add(entity);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Mercedeses.Find(id);

            if (product == null) return NotFound();

            LoadBrands();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<MercedesDto>(product));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MercedesDto model2)
        {
            if (!ModelState.IsValid)
            {
                LoadBrands();
                ViewBag.CreateMode = false;
                return View("Upsert", model2);
            }

            if (model2.Image != null)
                model2.ImgUrl = await fileService.EditProductImage(model2.ImgUrl, model2.Image);

            context.Mercedeses.Update(mapper.Map<Mercedes>(model2));
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = Roles.ADMIN)]
        public IActionResult Delete(int id)
        {
            var product = context.Mercedeses.Find(id);

            if (product == null) return NotFound(); // 404

            if (product.ImgUrl != null) 
                fileService.DeleteProductImage(product.ImgUrl);

            context.Mercedeses.Remove(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        private void LoadBrands()
        {
            // ViewBag - collection of properties that is accessible in View
            ViewBag.Brands = new SelectList(context.BrandOfCars.ToList(), "Id", "Name");
        }
    }
}
