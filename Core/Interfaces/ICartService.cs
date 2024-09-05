using _03_SecondHomeWorkViewModel.Entities;
using Core.Dtos;
using Data.Entities;
using System.Xml.Serialization;

namespace Core.Services
{
    public interface ICartService
    {
        List<MercedesDto> GetProducts();
        List<Mercedes> GetProductsEntity();
        int GetCount();
        void Add(int id);
        void Remove(int id);
        void Clear();
    }
}
