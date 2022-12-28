using HotelManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Accounts.Any())
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
                        Name = "Nguyen Van A",
                        Phone = "0912345678",
                        Email = "nguyenvana@email.com",
                        CitizencardId = "12345678",
                        CountryCode = "VN",
                        Gender = "male",
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
                        Gender = "female",
                        CitizencardId = "12345678",
                        CountryCode = "VN",
                    },
                    new Customer
                    {
                        Name = "Nong Lam C",
                        Phone = "0123456789",
                        Email = "nonglamc@email.com",
                        Gender = "male",
                        CitizencardId = "12345678",
                        CountryCode = "VN",
                        Account = new Account
                        {
                            Username = "nonglamc",
                            Password = "1",
                            RoleName = "customer"
                        }
                    }
                );

                context.Employees.AddRange(
                    new Employee
                    {
                        Name = "Enployee 1",
                        Phone = "0912345678",
                        Gender = "male",
                        Address = "HCM",
                        Salary = 1000,
                        Account = new Account
                        {
                            Username = "emp1",
                            Password = "1",
                            RoleName = "employee"
                        }
                    },
                    new Employee
                    {
                        Name = "Employee 2",
                        Phone = "0988888888",
                        Gender = "female",
                        Address = "HCM",
                        Salary = 3000
                    },
                    new Employee
                    {
                        Name = "Employee 3",
                        Phone = "0123456789",
                        Gender = "male",
                        Address = "HCM",
                        Salary = 2000,
                        Account = new Account
                        {
                            Username = "emp3",
                            Password = "1",
                            RoleName = "employee"
                        }
                    }
                );

                context.Facilities.AddRange(
                    new Facility()
                    {
                        Name = "Dieu hoa",
                        ImageUrl = "default_img.png"
                    },
                    new Facility()
                    {
                        Name = "Tivi",
                        ImageUrl = "default_img.png"
                    },
                    new Facility()
                    {
                        Name = "Tu lanh",
                        ImageUrl = "default_img.png"
                    },
                    new Facility()
                    {
                        Name = "Giuong",
                        ImageUrl = "default_img.png"
                    },
                    new Facility()
                    {
                        Name = "Ghe",
                        ImageUrl = "default_img.png"
                    }
                );


                RoomTypeImage[] roomTypeImages =
                {
                    new RoomTypeImage()
                    {
                        ImageName="Image1",
                        ImageUrl="default_img.png",
                        CreateDate = DateTime.Now
                    },
                    new RoomTypeImage()
                    {
                        ImageName="Image22",
                        ImageUrl="default_img.png",
                        CreateDate = DateTime.Now
                    },
                    new RoomTypeImage()
                    {
                        ImageName="Image333",
                        ImageUrl="default_img.png",
                        CreateDate = DateTime.Now
                    }
                };

                context.RoomTypes.AddRange(
                    new RoomType()
                    {
                        Name = "Phong thuong 2 nguoi",
                        Price = 500,
                        Quantity = 5,
                        MaxAdult = 2,
                        MaxChild = 2,
                        Description = "Phong nay rat cheap",
                        RoomTypeImages = new List<RoomTypeImage>(new RoomTypeImage[]{
                            new RoomTypeImage()
                            {
                                ImageName="Image1",
                                ImageUrl="default_img.png",
                                CreateDate = DateTime.Now
                            },
                            new RoomTypeImage()
                            {
                                ImageName="Image22",
                                ImageUrl="default_img2.png",
                                CreateDate = DateTime.Now
                            }
                        }),
                        FacilityApplies = new List<FacilityApply>(new FacilityApply[]{
                            new FacilityApply()
                            {
                                FacId=1,
                                Amount=1
                            },
                            new FacilityApply()
                            {
                                FacId=2,
                                Amount=2
                            },
                            new FacilityApply()
                            {
                                FacId=3,
                                Amount=3
                            }
                        }),
                        Rooms = new List<Room>(new Room[]
                        {
                            new Room()
                            {
                                RoomNo="A101",
                                Description="HELLO WORLD"
                            },
                            new Room()
                            {
                                RoomNo="A102",
                                Description="HELLO WORLD"
                            },
                            new Room()
                            {
                                RoomNo="A103",
                                Description="HELLO WORLD"
                            }
                        })
                    },
                    new RoomType()
                    {
                        Name = "Phong vip 2 nguoi",
                        Price = 2000,
                        Quantity = 3,
                        MaxAdult = 2,
                        MaxChild = 2,
                        Description = "Phong nay rat expensive",
                        RoomTypeImages = new List<RoomTypeImage>(new RoomTypeImage[]{
                            new RoomTypeImage()
                            {
                                ImageName="Image22",
                                ImageUrl="default_img2.png",
                                CreateDate = DateTime.Now
                            },
                            new RoomTypeImage()
                            {
                                ImageName="Image333",
                                ImageUrl="default_img.png",
                                CreateDate = DateTime.Now
                            }
                        }),
                        FacilityApplies = new List<FacilityApply>(new FacilityApply[]{
                            new FacilityApply()
                            {
                                FacId=1,
                                Amount=1
                            },
                            new FacilityApply()
                            {
                                FacId=3,
                                Amount=3
                            }
                        }),
                        Rooms = new List<Room>(new Room[]
                        {
                            new Room()
                            {
                                RoomNo="A201",
                                Description="HELLO"
                            },
                            new Room()
                            {
                                RoomNo="A202",
                                Description="HELLO"
                            },
                            new Room()
                            {
                                RoomNo="A203",
                                Description="HELLO"
                            }
                        })
                    },
                    new RoomType()
                    {
                        Name = "Phong thuong 4 nguoi",
                        Price = 2200,
                        Quantity = 4,
                        MaxAdult = 4,
                        MaxChild = 4,
                        Description = "Phong nay on ap",
                        RoomTypeImages = new List<RoomTypeImage>(new RoomTypeImage[]{
                            new RoomTypeImage()
                            {
                                ImageName="Image22",
                                ImageUrl="default_img.png",
                                CreateDate = DateTime.Now
                            }
                        }),
                        FacilityApplies = new List<FacilityApply>(new FacilityApply[]{
                            new FacilityApply()
                            {
                                FacId=3,
                                Amount=3
                            }
                        }),
                        Rooms = new List<Room>(new Room[]
                        {
                            new Room()
                            {
                                RoomNo="A301",
                                Description="WORLD"
                            }
                        })
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
