using Data.Entities;

namespace _03_SecondHomeWorkViewModel.Entities
{
    public class Mercedes
    {
        public int Id { get; set; }
        //[Required]
        public int BrandOfCarId { get; set; }
        public BrandOfCar? BrandOfCar { get; set; }

        //[Required]
        public string Model { get; set; }
        //[Url]
        public string? ImgUrl { get; set; }
        //[Required]
        public int Price { get; set; }
        public string? Class { get; set; }
        public int Year { get; set; }
        public double Volume { get; set; }
        public int HorsePower { get; set; }
        public int Discount { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
