﻿using HotelManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if(context.Accounts.Any())
                {
                    return; 
                }

                //Create Manager account
                context.Accounts.AddRange(
                        new Account
                        {
                            Username = "manager",
                            Password = "1",
                            RoleName = "manager",
                            CreateDate = DateTime.Now
                        }
                    );

                //Create customer accounts
                context.Customers.AddRange(
                    new Customer
                    {
                        Name="Nguyen Van A",
                        Phone="0912345678",
                        Email="nguyenvana@email.com",
                        Account = new Account
                        {
                            Username = "nguyenvana",
                            Password = "1",
                            RoleName = "customer"
                        }
                    },
                    new Customer
                    {
                        Name = "Le Thi B",
                        Phone = "0988888888",
                        Email = "lethib@email.com",
                    },
                    new Customer
                    {
                        Name = "Nong Lam C",
                        Phone = "0123456789",
                        Email = "nonglamc@email.com",
                        Account = new Account
                        {
                            Username = "nonglamc",
                            Password = "1",
                            RoleName = "customer"
                        }
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
