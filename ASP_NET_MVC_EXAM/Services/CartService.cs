using _03_SecondHomeWorkViewModel.Entities;
using AutoMapper;
using Core.Dtos;
using Core.Services;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ShopMvcApp_PD211.Extensions;

namespace ShopMvcApp_PD211.Services
{
    public class CartService : ICartService
    {
        private readonly HttpContext httpContext;
        private readonly IMapper mapper;
        private readonly CatalogDbContext context;

        public CartService(IHttpContextAccessor contextAccessor, IMapper mapper, CatalogDbContext context)
        {
            httpContext = contextAccessor.HttpContext!;
            this.mapper = mapper;
            this.context = context;
        }

        public List<MercedesDto> GetProducts()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            var products = context.Mercedeses.Include(x => x.BrandOfCar).Where(x => ids.Contains(x.Id)).ToList();

            return mapper.Map<List<MercedesDto>>(products);
        }

        public List<Mercedes> GetProductsEntity()
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items") ?? new();

            return context.Mercedeses.Include(x => x.BrandOfCar).Where(x => ids.Contains(x.Id)).ToList();
        }

        public int GetCount()
        {
            // зчитуємо наявні елементи в корзині
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            // якщо елементів немає, тоді створюємо порожній список
            if (ids == null) return 0;

            return ids.Count;
        }

        public void Add(int id)
        {
            // зчитуємо наявні елементи в корзині
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            // якщо елементів немає, тоді створюємо порожній список
            if (ids == null) ids = new();
            // додаємо новий елемент
            ids.Add(id);

            // зберігаємо оновлений список корзини в cookies
            httpContext.Session.Set("cart_items", ids);
        }

        public void Remove(int id)
        {
            // зчитуємо наявні елементи в корзині
            var ids = httpContext.Session.Get<List<int>>("cart_items");

            // якщо елементів немає, тоді створюємо порожній список
            if (ids == null) return; // TODO: throw exception

            // додаємо новий елемент
            ids.Remove(id);

            // зберігаємо оновлений список корзини в cookies
            httpContext.Session.Set("cart_items", ids);
        }

        public void Clear()
        {
            httpContext.Session.Remove("cart_items");
        }
    }
}
