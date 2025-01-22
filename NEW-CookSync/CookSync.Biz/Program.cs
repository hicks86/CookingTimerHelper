using CookSync.Biz;
using CookSync.Dal;
using CookSync.Dal.DataAccessLayer;
using Csla;
using Csla.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class Program
{
    public static IHost Host { get; private set; }

    public static void Main(string[] args)
    {
        var serviceProvider = CreateServiceProvider();

        var dpFactory = serviceProvider.GetRequiredService<IDataPortalFactory>();

        // Initialize the list to store food items
        var foodItems = new List<string>();

        Console.WriteLine("Add your food items. Press CTRL+S to finish.");

        var menu = dpFactory.GetPortal<MenuFood>().Create();

        var foodItemPortal = dpFactory.GetPortal<FoodItemEdit>();

        while (true)
        {
            // Ask the user to add a food item
            Console.Write("Enter a food item: ");
            string input = Console.ReadLine();

            var foodItem = foodItemPortal.Create(input);

            Console.WriteLine("Add another food item? (Press ENTER to add another, CTRL+G to finish)");

            menu.Add(foodItem);

            // Check if CTRL+S is pressed
            ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true to not show the key in the console
            if (keyInfo.Key == ConsoleKey.G && keyInfo.Modifiers == ConsoleModifiers.Control)
            {
                menu.Save();
                break; // Exit the loop if CTRL+S is pressed
            }
        }

        // After the loop, continue with the rest of the code
        Console.WriteLine("\nFood items added:");
        foreach (var item in menu)
        {
            Console.WriteLine(item);
        }
    }

    public static IServiceProvider CreateServiceProvider()
            => new ServiceCollection()
                    .AddCsla()
                    .AddSingleton<IFoodMenuDal, InMemoryFoodMenuRepository>()
                    //.AddSingleton<IStyleService>((s) => new StyleService(new StyleEventSqlLiteRepository(Path.Combine("C:\\Users\\RhysHicks\\AppData\\Local\\PerfectIt", "PerfectIt.Styles.sdb"))))
                    //.AddSingleton<ILicenseModel>((s) => new LicenseModel())
                    .BuildServiceProvider();
}

