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

        var products = new Product[]
        {
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Headphones ZX-129",
                ProductDescription =
                    "Incredible headphones! Listen to the music all day long and enjoy the frequencies!",
                Currency = "EUR", ProductPrice = 99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Headphones ZX-129",
                ProductDescription =
                    "Incredible headphones! Listen to the music all day long and enjoy the frequencies!",
                Currency = "EUR", ProductPrice = 99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Smartphone ABC-10",
                ProductDescription = "The latest smartphone with advanced features.", Currency = "EUR",
                ProductPrice = 899.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Laptop XYZ-500",
                ProductDescription = "Powerful laptop for work and play.", Currency = "USD", ProductPrice = 1499
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Camera DSLR-2000",
                ProductDescription = "High-quality DSLR camera for professional photography.", Currency = "USD",
                ProductPrice = 1299.50
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Fitness Tracker Fit-100",
                ProductDescription = "Monitor your fitness goals with this advanced tracker.", Currency = "EUR",
                ProductPrice = 79.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Gaming Console X-Box Z",
                ProductDescription = "Experience gaming like never before with the X-Box Z.", Currency = "USD",
                ProductPrice = 449
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Wireless Earbuds Ultra",
                ProductDescription = "Enjoy wireless freedom with these high-quality earbuds.", Currency = "EUR",
                ProductPrice = 129.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "4K Smart TV Vizio 55\"",
                ProductDescription = "Bring cinematic experience to your living room with this 4K Smart TV.",
                Currency = "USD", ProductPrice = 799.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Bluetooth Speaker SoundWave",
                ProductDescription = "Portable speaker with powerful sound quality.", Currency = "EUR",
                ProductPrice = 59.50
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Coffee Machine Espresso Pro",
                ProductDescription = "Brew professional-grade espresso at home with this coffee machine.",
                Currency = "USD", ProductPrice = 249
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Electric Scooter E-Sprint",
                ProductDescription = "Efficient and eco-friendly electric scooter for urban commuting.",
                Currency = "EUR", ProductPrice = 599
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Power Bank ChargeMaster 10000mAh",
                ProductDescription = "Never run out of battery with this high-capacity power bank.", Currency = "USD",
                ProductPrice = 39.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Wireless Mouse Speedster",
                ProductDescription = "Ergonomic wireless mouse for smooth navigation.", Currency = "EUR",
                ProductPrice = 29.95
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Home Security Camera Kit",
                ProductDescription = "Keep your home secure with this advanced camera kit.", Currency = "USD",
                ProductPrice = 349.50
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Gaming Keyboard RGB-300",
                ProductDescription = "Customize your gaming experience with this RGB keyboard.", Currency = "EUR",
                ProductPrice = 119
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Portable Blender SmoothieMaster",
                ProductDescription = "Blend delicious smoothies on-the-go with this portable blender.",
                Currency = "USD", ProductPrice = 49.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Smart Thermostat EcoSaver",
                ProductDescription = "Efficiently control your home's temperature with this smart thermostat.",
                Currency = "EUR", ProductPrice = 129.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Noise-Canceling Headphones Elite",
                ProductDescription = "Immerse yourself in music with these high-end noise-canceling headphones.",
                Currency = "USD", ProductPrice = 299
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Wireless Charger Duo",
                ProductDescription = "Charge your devices wirelessly with this sleek charger.", Currency = "EUR",
                ProductPrice = 49.50
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Robot Vacuum CleanBot",
                ProductDescription = "Let this smart robot handle your cleaning tasks.", Currency = "USD",
                ProductPrice = 349
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Digital Drawing Tablet ArtMaster",
                ProductDescription = "Unleash your creativity with this advanced drawing tablet.", Currency = "EUR",
                ProductPrice = 179.99
            },
            new()
            {
                Id = Guid.NewGuid(), ProductName = "Electric Toothbrush SonicClean",
                ProductDescription = "Achieve a brighter smile with this powerful electric toothbrush.",
                Currency = "USD", ProductPrice = 69.95
            }
        };

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}