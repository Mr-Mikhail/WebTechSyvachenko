using OnlineShop.Domain.Models;

namespace OnlineShop.Persistence;

public static class DbInitializer
{
    public static void Initialize(DefaultContext context)
    {
        if (context.Products.Any())
        {
            return;
        }

        var products = new Dish[]
        {
            new()
            {
                Name = "Kebab",
                Description = "Grilled meat, often served with rice, vegetables, and yogurt sauce.",
                Price = 6.99,
                Categories = new List<Category>
                {
                    new() { Name = "Main Course" },
                    new() { Name = "Grilled" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "John Doe",
                        Value = "Delicious!",
                        Stars = 5
                    }
                }
            },
            new()
            {
                Name = "Baklava",
                Description =
                    "A sweet pastry made of layers of filo filled with chopped nuts and sweetened with syrup or honey.",
                Price = 4.50,
                Categories = new List<Category>
                {
                    new() { Name = "Dessert" },
                    new() { Name = "Pastry" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "Jane Smith",
                        Value = "Amazing dessert!",
                        Stars = 5
                    }
                }
            },
            new()
            {
                Name = "Iskender",
                Description = "Sliced doner kebab served on a bed of pita bread, topped with tomato sauce and yogurt.",
                Price = 9.99,
                Categories = new List<Category>
                {
                    new() { Name = "Main Course" },
                    new() { Name = "Grilled" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "Michael Johnson",
                        Value = "Absolutely delicious!",
                        Stars = 5
                    }
                }
            },
            new()
            {
                Name = "Meze Platter",
                Description =
                    "An assortment of small, flavorful dishes such as hummus, tzatziki, and stuffed grape leaves.",
                Price = 12.50,
                Categories = new List<Category>
                {
                    new() { Name = "Appetizer" },
                    new() { Name = "Vegetarian" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "Anna Davis",
                        Value = "Great variety of flavors!",
                        Stars = 4
                    }
                }
            },
            new()
            {
                Name = "Manti",
                Description = "Tiny dumplings filled with spiced meat, served with yogurt and garlic sauce.",
                Price = 10.99,
                Categories = new List<Category>
                {
                    new() { Name = "Main Course" },
                    new() { Name = "Dumplings" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "David Brown",
                        Value = "Authentic and flavorful!",
                        Stars = 5
                    }
                }
            },
            new()
            {
                Name = "Turkish Delight",
                Description = "A sweet confection made of starch and sugar, often flavored with rosewater or citrus.",
                Price = 6.50,
                Categories = new List<Category>
                {
                    new() { Name = "Dessert" },
                    new() { Name = "Candy" }
                },
                Currency = "EUR",
                Reviews = new List<Review>
                {
                    new()
                    {
                        Name = "Emily White",
                        Value = "Delightful treat!",
                        Stars = 4
                    }
                }
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}