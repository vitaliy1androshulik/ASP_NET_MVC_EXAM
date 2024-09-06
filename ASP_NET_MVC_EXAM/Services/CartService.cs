using _03_SecondHomeWorkViewModel.Entities;
using AutoMapper;
using Core.Dtos;
using Core.Services;
using Data;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using ASP_NET_MVC_EXAM.Extensions;

namespace ASP_NET_MVC_EXAM.Services
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
            var ids = httpContext.Session.Get<List<int>>("cart_items");
            if (ids == null) return 0;

            return ids.Count;
        }

        public void Add(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");
            if (ids == null) ids = new();
            ids.Add(id);
            httpContext.Session.Set("cart_items", ids);
        }

        public void Remove(int id)
        {
            var ids = httpContext.Session.Get<List<int>>("cart_items");
            if (ids == null) return; // TODO: throw exception
            ids.Remove(id);
            httpContext.Session.Set("cart_items", ids);
        }

        public void Clear()
        {
            httpContext.Session.Remove("cart_items");
        }
    }
}
