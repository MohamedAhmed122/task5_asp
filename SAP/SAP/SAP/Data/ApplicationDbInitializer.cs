using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SAP.Data.Models.Catalogue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAP.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string UserId = "12345678-ffff-aaaa-bbbb-035f6322e57c";
            string AdminRoleId = "87654321-ffff-aaaa-bbbb-035f6322e57c";

            if (roleManager.RoleExistsAsync("admin").Result == false)
            {
                IdentityRole role = new IdentityRole
                {
                    Id = AdminRoleId,
                    Name = "admin",
                    NormalizedName = "ADMIN"
                };

                roleManager.CreateAsync(role).Wait();
            }

            if (userManager.FindByEmailAsync("admin@sap.com").Result == null)
            {
                IdentityUser user = new IdentityUser
                {
                    Id = UserId,
                    UserName = "admin@sap.com",
                    NormalizedUserName = "ADMIN@SAP.COM",
                    Email = "admin@sap.com",
                    NormalizedEmail = "ADMIN@SAP.COM",
                };

                IdentityResult result = userManager.CreateAsync(user, "Admin_1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }

        public static void SeedCatalogue(ApplicationDbContext dbContext)
        {
            string smartphonesCategoryName = "Smartphones";
            string androidCategory = "Android";
            string iOSCategory = "iOS";
            string harmonyCategory = "Harmony";
            string PCCategoryName = "PC";
            string laptopsCategoryName = "Laptops";
            string TVCategoryName = "TV";

            if (dbContext.Categories.Any())
            {
                return;
            }

            var smarts = new Category { Name = smartphonesCategoryName };
            dbContext.Categories.Add(smarts);
            var android = new Category { Name = androidCategory, Parent = smarts };
            android.Items = new List<Item>
                {
                    new Item {
                        Name = "Samsung Galaxy A10",
                        ManufacturerId = "SM-A105FZKGSER",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st4/fit/190/190/cfdeafa5e804f8f26822c5e914ac1b1e/db608f8fcee79c85b1142927f16541bfe9bf70aba8b0f817cfb6ce6207269bd1.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 10},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 9},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 8}
                        }
                    },
                    new Item {
                        Name = "Samsung Galaxy A20",
                        ManufacturerId = "SM-A205FZKVSER",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st1/fit/190/190/f481ca532fa8783aca3595afb12adc02/ad3661793efad7c2526db8c2e83fcceda96e50377040dc465d5cdef01ab146a7.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 15},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 14},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 13}
                        }
                    },
                    new Item {
                        Name = "Samsung Galaxy A30",
                        ManufacturerId = "SM-A305FZKUSER",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st1/fit/190/190/0bc76e216b87412b00f933c8c24a0237/04ecc6b51e2e7df1251393dde2ed00ad135178a4d511614178a296f0cf19dc19.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 20},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 19},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 18}
                        }
                    }
                };
            dbContext.Categories.Add(android);
            var ios = new Category { Name = iOSCategory, Parent = smarts };
            ios.Items = new List<Item>
                {
                    new Item {
                        Name = "Iphone X",
                        ManufacturerId ="FQAF2RU/A",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st4/fit/190/190/433f590f06b036d2c17029bc35ad9355/817a9606bd42fd2a7770bb7569310f9aededd44ecd5fa95a616b23d446b85fa2.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 50},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 47},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 45}
                        }
                    },
                    new Item {
                        Name = "Iphone XI",
                        ManufacturerId ="MWM02RU/A",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st4/fit/190/190/03eb616bf8dbfb1da4029747aa930d0d/6b6a05185029b867d2f0657bb7037583ef968d6e8d9bdebddd9fef7f93a443d4.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 70},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 67},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 65}
                        }
                    }
                };
            dbContext.Categories.Add(ios);
            var harmony = new Category { Name = harmonyCategory, Parent = smarts };
            harmony.Items = new List<Item>
                {
                    new Item {
                        Name = "Huawei P30",
                        ManufacturerId = "51093QXN",
                        PictureUrl = "https://c.dns-shop.ru/thumb/st4/fit/190/190/e92ce4b3e8a9f81943bc973cabba3542/26ea1eb198510834d2a206f16ea4015770adba4a0d1e6f702718af0e2ca09fe1.jpg.webp",
                        QuantityInStock = 100,
                        Attributes = new List<Attributes> {
                            new Attributes { Color = Colors.Black, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 15},
                            new Attributes { Color = Colors.White, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 14},
                            new Attributes { Color = Colors.Green, Length = 1, Weight = 1, Width = 1, Height = 1, Price = 13}
                        }
                    }
                };
            dbContext.Categories.Add(harmony);
            var pc = new Category
            {
                Name = PCCategoryName,
                Childs = new List<Category>
                {
                    new Category
                    {
                        Name = "Parts",
                        Childs = new List<Category>
                        {
                            new Category { Name = "RAM" },
                            new Category { Name = "HDDs" },
                            new Category { Name = "SSDs" },
                            new Category { Name = "Cases" }
                        }
                    },
                    new Category {  Name = "Ready PC" }
                }
            };
            dbContext.Categories.Add(pc);
            dbContext.Categories.Add(new Category { Name = laptopsCategoryName });
            dbContext.Categories.Add(new Category { Name = TVCategoryName });
            dbContext.SaveChanges();
        }
    }
}
