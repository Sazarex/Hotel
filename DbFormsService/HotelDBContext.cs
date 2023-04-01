using DbFormsService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DbFormsService.GlobalVariables;

namespace DbFormsService
{
    public class HotelDBContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Client> Clients{ get; set; }
        public DbSet<Payment> Payments{ get; set; }
        public HotelDBContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Username = postgres; Password = 806959; Host = localhost; Port = 5432; Database = Hotel; ");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>().Property(u => u.Id)
                                 .UseIdentityAlwaysColumn()
                                 .HasIdentityOptions(startValue: 11);

            modelBuilder.Entity<Employee>().Property(u => u.Id)
                                 .UseIdentityAlwaysColumn()
                                 .HasIdentityOptions(startValue: 100);


            modelBuilder.Entity<Client>().Property(u => u.Id)
                                 .UseIdentityAlwaysColumn()
                                 .HasIdentityOptions(startValue: 100);

            #region Seed of employee
            var account1 = new Employee()
            {
                Id = 1,
                Name = "Геннадий Артёмов",
                Post = Post.Administator
            };

            var account2 = new Employee()
            {
                Id = 2,
                Name = "Вероника Арбузова",
                Post = Post.Manager
            };

            var account3 = new Employee()
            {
                Id = 3,
                Name = "Иван Шар",
                Post = Post.Manager
            };

            var account4 = new Employee()
            {
                Id = 4,
                Name = "Анастасия Радушная",
                Post = Post.Manager
            };

            var account5 = new Employee()
            {
                Id = 5,
                Name = "Владимир Мономах",
                Post = Post.Manager
            };

            var account6 = new Employee()
            {
                Id = 6,
                Name = "Ландыш Иванова",
                Post = Post.Manager
            };

            var account7 = new Employee()
            {
                Id = 7,
                Name = "Пётр Покорный",
                Post = Post.Manager
            };

            modelBuilder.Entity<Employee>().HasData(new Employee[]
            {
                account1,account2,account3,account4,account5,account6,account7
            });
            #endregion

            #region Seed of Rooms
            var room1 = new Room()
            {
                Id = 1,
                IsTaken = false,
                Number = 1,
                IsReserved = false
            };

            var room2 = new Room()
            {
                Id = 2,
                IsTaken = false,
                Number = 2,
                IsReserved = false
            };
            var room3 = new Room()
            {
                Id = 3,
                IsTaken = false,
                Number = 3,
                IsReserved = false
            };
            var room4 = new Room()
            {
                Id = 4,
                IsTaken = false,
                Number = 4,
                IsReserved = false
            };
            var room5 = new Room()
            {
                Id = 5,
                IsTaken = false,
                Number = 5,
                IsReserved = false
            };
            var room6 = new Room()
            {
                Id = 6,
                IsTaken = false,
                Number = 6,
                IsReserved = false
            };
            var room7 = new Room()
            {
                Id = 7,
                IsTaken = false,
                Number = 7,
                IsReserved = false
            };
            var room8 = new Room()
            {
                Id = 8,
                IsTaken = false,
                Number = 8,
                IsReserved = false
            };
            var room9 = new Room()
            {
                Id = 9,
                IsTaken = false,
                Number = 9,
                IsReserved = false
            };
            var room10 = new Room()
            {
                Id = 10,
                IsTaken = false,
                Number = 10,
                IsReserved = false
            };


            modelBuilder.Entity<Room>().HasData(new Room[]
            {
                room1,room2,room3,room4,room5,room6,room7,room8,room9,room10,
            });
            #endregion

            #region Seed of Clients

            var client1 = new Client()
            {
                Id = 1,
                Name = "Артур Богатый"
            };

            var client2 = new Client()
            {
                Id = 2,
                Name = "Семён Джава"
            };

            var client3 = new Client()
            {
                Id = 3,
                Name = "Каринус Альф"
            };
            var client4 = new Client()
            {
                Id = 4,
                Name = "Геннайдий Свекровкин"

            };

            modelBuilder.Entity<Client>().HasData(new Client[]
            {
                client1,client2,client3,client4
            });
            #endregion

        }
    }
}
