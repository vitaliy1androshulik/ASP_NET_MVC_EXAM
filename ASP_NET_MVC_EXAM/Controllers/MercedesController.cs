using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data;
using Core.Dtos;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using ASP_NET_MVC_EXAM.Extensions;
using _03_SecondHomeWorkViewModel.Entities;

namespace ASP_NET_MVC_EXAM.Controllers
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
            var mercedes = context.Mercedeses.Find(id);

            if (mercedes == null) return NotFound();

            return View(mapper.Map<MercedesDto>(mercedes));
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
            if (model2.Image != null)
                model2.ImgUrl = await fileService.SaveProductImage(model2.Image);

            var entity = mapper.Map<Mercedes>(model2);

            context.Mercedeses.Add(entity);
            context.SaveChanges();

            return RedirectToAction("Catalog");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var mercedes = context.Mercedeses.Find(id);

            if (mercedes == null) return NotFound();

            LoadBrands();
            ViewBag.CreateMode = false;
            return View("Upsert", mapper.Map<MercedesDto>(mercedes));
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

            return RedirectToAction("Catalog");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var mercedes = context.Mercedeses.Find(id);

            if (mercedes == null) return NotFound();

            if (mercedes.ImgUrl != null) 
                fileService.DeleteProductImage(mercedes.ImgUrl);

            context.Mercedeses.Remove(mercedes);
            context.SaveChanges();

            return RedirectToAction("Catalog");
        }

        private void LoadBrands()
        {
            ViewBag.Brands = new SelectList(context.BrandOfCars.ToList(), "Id", "Name");
        }
    }
}
