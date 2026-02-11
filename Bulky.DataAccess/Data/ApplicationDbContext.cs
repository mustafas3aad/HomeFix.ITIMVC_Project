using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 
        {
            
        }
        public DbSet<Service> Services { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<BookingCart> BookingCarts { get; set; }
        public DbSet<TeamImages> TeamImages { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            base.OnModelCreating(modelBuilder);
            // Seed Data for Company
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = 1,
                    Name = "Mahmoud",
                    StreetAddress = "Main Street",
                    City = "Tanta",
                    State = "Gharbia",
                    PhoneNumber = "01012345678"
                },
                new Company
                {
                    Id = 2,
                    Name = "Mostafa",
                    StreetAddress = "Central Street",
                    City = "Kafr El Sheikh",
                    State = "Kafr El Sheikh",
                    PhoneNumber = "01098765432"
                },
                new Company
                {
                    Id = 3,
                    Name = "Soheila",
                    StreetAddress = "Downtown",
                    City = "Ismailia",
                    State = "Ismailia",
                    PhoneNumber = "01123456789"
                },
                new Company
                {
                    Id = 4,
                    Name = "Menna",
                    StreetAddress = "City Center",
                    City = "Ismailia",
                    State = "Ismailia",
                    PhoneNumber = "01134567890"
                },
                new Company
                {
                    Id = 5,
                    Name = "Nour",
                    StreetAddress = "Main Square",
                    City = "Tanta",
                    State = "Gharbia",
                    PhoneNumber = "01234567891"
                }
            );


            // Seed Data for Service
            // Seed Data for Service
            modelBuilder.Entity<Service>().HasData(
      new Service { Id = 1, Name = "Plumbing", DisplayOrder = 1 },
      new Service { Id = 2, Name = "Electrical", DisplayOrder = 2 },
      new Service { Id = 3, Name = "Carpentry", DisplayOrder = 3 },
      new Service { Id = 4, Name = "HVAC Maintenance", DisplayOrder = 4 },
      new Service { Id = 5, Name = "Painting", DisplayOrder = 5 },
      new Service { Id = 6, Name = "Cleaning", DisplayOrder = 6 },
      new Service { Id = 7, Name = "Home Security Systems", DisplayOrder = 7 },
      new Service { Id = 8, Name = "Garden & Lawn Care", DisplayOrder = 8 },
      new Service { Id = 9, Name = "Home Renovation", DisplayOrder = 9 }
  );

            // Seed Data for Team
            modelBuilder.Entity<Team>().HasData(
     new Team
     {
         Id = 1,
         Title = "Plumbing",
         Description = "The undisputed leaders in plumbing solutions.",
         YearsOfExperience = 15,
         IsAvailable24Hours = true,
         DepositAmount = 500,
         DepositNote = "Non-refundable priority booking fee.",
         HourlyRate = 250,
         ServiceId = 1,
         WorkersCount = 5
     },
     new Team
     {
         Id = 2,
         Title = "Electrical",
         Description = "Certified professionals for all electrical works.",
         YearsOfExperience = 12,
         IsAvailable24Hours = true,
         DepositAmount = 600,
         DepositNote = "Covers initial safety diagnostic.",
         HourlyRate = 300,
         ServiceId = 2,
         WorkersCount = 4
     },
     new Team
     {
         Id = 3,
         Title = "Carpentry",
         Description = "High-end custom carpentry solutions.",
         YearsOfExperience = 18,
         IsAvailable24Hours = false,
         DepositAmount = 700,
         DepositNote = "Required for customized design assessment.",
         HourlyRate = 280,
         ServiceId = 3,
         WorkersCount = 6
     },
     new Team
     {
         Id = 4,
         Title = "HVAC Maintenance",
         Description = "Advanced cooling and heating systems maintenance.",
         YearsOfExperience = 10,
         IsAvailable24Hours = true,
         DepositAmount = 550,
         DepositNote = "Inspection and gas level check included.",
         HourlyRate = 320,
         ServiceId = 4,
         WorkersCount = 4
     },
     new Team
     {
         Id = 5,
         Title = "Painting",
         Description = "Premium interior and exterior painting services.",
         YearsOfExperience = 9,
         IsAvailable24Hours = false,
         DepositAmount = 500,
         DepositNote = "Secures your slot in our schedule.",
         HourlyRate = 220,
         ServiceId = 5,
         WorkersCount = 8
     },
     new Team
     {
         Id = 6,
         Title = "Cleaning",
         Description = "Deep cleaning with professional equipment.",
         YearsOfExperience = 7,
         IsAvailable24Hours = false,
         DepositAmount = 500,
         DepositNote = "Applicable towards final service cost.",
         HourlyRate = 200,
         ServiceId = 6,
         WorkersCount = 12
     },
    new Team
    {
        Id = 7,
        Title = "Home Security Systems",
        Description = "Installation and maintenance of home security systems including cameras, alarms, and smart locks.",
        YearsOfExperience = 9,
        IsAvailable24Hours = true,
        DepositAmount = 900,
        DepositNote = "Non-refundable security system assessment fee.",
        HourlyRate = 450,
        ServiceId = 7,
        WorkersCount = 4
    },
new Team
{
    Id = 8,
    Title = "Garden & Lawn Care",
    Description = "Professional garden maintenance, lawn trimming, and irrigation system setup.",
    YearsOfExperience = 12,
    IsAvailable24Hours = false,
    DepositAmount = 700,
    DepositNote = "Covers site visit and garden assessment.",
    HourlyRate = 300,
    ServiceId = 8,
    WorkersCount = 6
},
new Team
{
    Id = 9,
    Title = "Home Renovation",
    Description = "Complete home renovation services including tiling, painting, and minor structural repairs.",
    YearsOfExperience = 16,
    IsAvailable24Hours = false,
    DepositAmount = 1000,
    DepositNote = "Initial inspection and renovation planning fee.",
    HourlyRate = 500,
    ServiceId = 9,
    WorkersCount = 8
}
 );

        }
    }
}
