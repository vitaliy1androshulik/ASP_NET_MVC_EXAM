namespace _03_SecondHomeWorkViewModel.Entities
{
    public class BrandOfCar
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Mercedes> Mercedeses { get; set; }
    }
}
