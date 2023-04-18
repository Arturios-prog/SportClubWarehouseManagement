using Duende.IdentityServer.EntityFramework.Options;
using SportClubWMS.Shared;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportClubWarehouseManagement.Server.Models;

namespace SportClubAPI.Data.Repositories
{
    public class AppDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<SportGood> SportGoods { get; set; }
        public DbSet<CustomerSportGood> CustomerSportGoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 1,
                FirstName = "Tom",
                SecondName = "Jackson",
                Gender = Gender.Male,
                Age = 24,
                SportGoods = new List<SportGood>(),
                Address = "St.Johnes St, 57",
                SubscribeStatus = SubscribeStatus.SubscribeGeneral,
                Email = "tom-jackson.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()

            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 2,
                FirstName = "Meina",
                SecondName = "Gladston",
                Age = 60,
                Gender = Gender.Female,
                SportGoods = new List<SportGood>(),
                Address = "TK-center, 5",
                SubscribeStatus = SubscribeStatus.SubscribePlus,
                Email = "meina-gladston.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 3,
                FirstName = "John",
                SecondName = "Batista",
                Gender = Gender.Male,
                Age = 50,
                SportGoods = new List<SportGood>(),
                Address = "Jackson St., 24",
                SubscribeStatus = SubscribeStatus.SubscribePlus,
                Email = "John-batista.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 4,
                FirstName = "Boris",
                SecondName = "Johnson",
                Gender = Gender.Male,
                Age = 52,
                SportGoods = new List<SportGood>(),
                Address = "Britain St., 77",
                SubscribeStatus = SubscribeStatus.SubscribeGeneral,
                Email = "Boris-Johnson.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 5,
                FirstName = "Vladislav",
                SecondName = "Kutepov",
                Gender = Gender.Male,
                Age = 22,
                SportGoods = new List<SportGood>(),
                Address = "Ladozhskaya St., 15",
                SubscribeStatus = SubscribeStatus.SubscribeGeneral,
                Email = "Vlad-Kutepov.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 6,
                FirstName = "Evgeniy",
                SecondName = "Bazhenov",
                Gender = Gender.Male,
                Age = 30,
                SportGoods = new List<SportGood>(),
                Address = "Moskovskaya St., 15",
                SubscribeStatus = SubscribeStatus.SubscribePlus,
                Email = "Vlad-Kutepov.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<Customer>().HasData(new Customer()
            {
                CustomerId = 7,
                FirstName = "Elena",
                SecondName = "Kapustovna",
                Gender = Gender.Female,
                Age = 40,
                SportGoods = new List<SportGood>(),
                Address = "Komsomolskaya St., 15",
                SubscribeStatus = SubscribeStatus.SubscribePlus,
                Email = "elena-kapustkina.@gmail.com",
                RegistrationDate = DateTime.Now,
                CustomerSportGoods = new List<CustomerSportGood>()
            });
            modelBuilder.Entity<SportGood>().HasData(new SportGood()
            {
                SportGoodId = 1,
                Name = "Football ball",
                Category = SportCategory.Football,
                Quantity = 20,
                Customers = new List<Customer>(),
                Description = "A ball that is used in a football game"
            });
            modelBuilder.Entity<SportGood>().HasData(new SportGood()
            {
                SportGoodId = 2,
                Name = "Basketball ball",
                Category = SportCategory.Basketball,
                Quantity = 15,
                Customers = new List<Customer>(),
                Description = "A ball that is used in a basketball game"
            });
            modelBuilder.Entity<SportGood>().HasData(new SportGood()
            {
                SportGoodId = 3,
                Name = "Slippers",
                Category = SportCategory.Swimming,
                Quantity = 25,
                Customers = new List<Customer>(),
                Description = "A good that is used for a faster swimming"
            });
            modelBuilder.Entity<SportGood>().HasData(new SportGood()
            {
                SportGoodId = 4,
                Name = "Tennis crocket",
                Category = SportCategory.Tennis,
                Quantity = 30,
                Customers = new List<Customer>(),
                Description = "It is used for punching a tennis ball"
            });
            modelBuilder.Entity<SportGood>().HasData(new SportGood()
            {
                SportGoodId = 5,
                Name = "Tennis ball",
                Category = SportCategory.Tennis,
                Quantity = 15,
                Customers = new List<Customer>(),
                Description = "A ball that is used in a tennis game"
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
