using Microsoft.AspNetCore.Http;

namespace Core.Dtos
{
    public class MercedesDto
    {
        public int? Id { get; set; }
        public int? BrandOfCarId { get; set; }
        public string? BrandOfCar { get; set; }
        public string Model { get; set; }
        public string? ImgUrl { get; set; }
        public IFormFile? Image { get; set; }
        public int Price { get; set; }
        public string? Class { get; set; }
        public int Year { get; set; }
        public double? Volume { get; set; }
        public int HorsePower { get; set; }
        public int Discount { get; set; }
    }
}
