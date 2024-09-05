using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using _03_SecondHomeWorkViewModel.Entities;

namespace Data
{
    public class CatalogDbContext : IdentityDbContext<User>
    {
        public DbSet<Mercedes> Mercedeses { get; set; }
        public DbSet<BrandOfCar> BrandOfCars { get; set; }
        public DbSet<Order> Orders { get; set; }

        public CatalogDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;" +
                "Initial Catalog=NewMercedesBenzDB;" +
                "Integrated Security=True;" +
                "Connect Timeout=30;" +
                "Encrypt=False;" +
                "Trust Server Certificate=False;" +
                "Application Intent=ReadWrite;" +
                "Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BrandOfCar>().HasData(new List<BrandOfCar>()
            {
                new BrandOfCar() { Id = 1, Name = "Mercedes-Benz" },
                new BrandOfCar() { Id = 2, Name = "Mercedes-AMG" },
            });

            modelBuilder.Entity<Mercedes>().HasData(new List<Mercedes>()
            {
                new Mercedes() {Id=1, BrandOfCarId=1, Model="E450 4MATIC",
                    ImgUrl="https://images.netdirector.co.uk/gforces-auto/image/upload/w_548,h_365,q_auto,c_fill,f_auto,fl_lossy/auto-client/5d18acbf1848174117e1b0223235a361/e_220_d_4matic.png",
                    Class="E-Class",
                Price=3735256, Discount=2, HorsePower=375, Volume=3.0, Year=2024},
                new Mercedes() {Id=2, BrandOfCarId=1, Model="E 220 d",
                    ImgUrl="https://images.netdirector.co.uk/gforces-auto/image/upload/w_548,h_365,q_auto,c_fill,f_auto,fl_lossy/auto-client/acfe7552173b3f6c863cee630a2345a0/e_220_d.png",Class="E-Class",
                Price=2823744, Discount=5, HorsePower=194, Volume=2.0, Year=2024},
                new Mercedes() {Id=3, BrandOfCarId=1, Model="A 180",
                    ImgUrl="https://images.netdirector.co.uk/gforces-auto/image/upload/w_548,h_365,q_auto,c_fill,f_auto,fl_lossy/auto-client/aae4042503cc00d78b7772f9c12f7271/a180.png", Class="A-Class",
                Price=1998166, Discount=2, HorsePower=136, Volume=1.95, Year=2024},
                new Mercedes() {Id=4, BrandOfCarId=2, Model="A 35 AMG 4MATIC",
                    ImgUrl="https://images.netdirector.co.uk/gforces-auto/image/upload/w_548,h_365,q_auto,c_fill,f_auto,fl_lossy/auto-client/3172c0ee5ae878080765fbc455e54e6e/a35.png",Class="A-Class",
                Price=2794320, Discount=0, HorsePower=306, Volume=2.0, Year=2024},
                new Mercedes() {Id=5, BrandOfCarId=1, Model="C 180", Class="C-Class",
                Price=2148646, Discount=15, HorsePower=168, Volume=1.5, Year=2024},
            });
        }
    }
}
