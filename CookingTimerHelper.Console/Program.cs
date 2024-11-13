using CookingTimerHelper;
using CookingTimerHelper.Biz;
using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.DataAccessLayer;
using Csla;
using Csla.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

internal class Program
{
	public static IHost Host { get; private set; }

	private static void Main(string[] args)
	{
		Host = new HostBuilder()
		.ConfigureServices((hostContext, services) => services
			.AddTransient(typeof(IFoodMenuDal), typeof(InMemoryFoodMenuRepository))
			.AddCsla()).Build();

		Console.WriteLine("Creating a menu");
		var menu = Host.Services.GetService<IDataPortal<MenuEdit>>().Create();
		Console.Write("Enter menu name: ");
		menu.Name = Console.ReadLine();

		if (menu.IsSavable)
		{
			menu = menu.Save();
			Console.WriteLine($"Added menu {menu.Name} with Id {menu.Id}");
		}
		else
		{
			Console.WriteLine("Invalid entry");
			foreach (var item in menu.BrokenRulesCollection)
			{
				Console.WriteLine(item.Description);
			}

			Console.ReadKey();
			return;
		}

		bool finish = true;

		while (finish)
		{
			Console.WriteLine("Adding food to the menu");
			var foodItem = Host.Services.GetService<IDataPortal<FoodItemEdit>>().Create();
			Console.Write("Enter food name: ");
			foodItem.Name = Console.ReadLine();
			Console.Write("Enter time to cook: ");
			foodItem.TimeToCook = TimeSpan.Parse( Console.ReadLine());
			Console.Write("Enter temperature: ");
			foodItem.Temperature = int.Parse(Console.ReadLine());

			if (foodItem.IsSavable)
			{
				//foodItem = foodItem.Save();
				menu.FoodItems.Add(foodItem);
			}

			Console.WriteLine("Press C to Continue, or X to Exit");
			var keyPressed = Console.ReadKey();
			finish = !(keyPressed.Key == ConsoleKey.X);
		}

		if (menu.IsSavable)
		{
			menu = menu.Save();
		}

		Console.WriteLine($"Current Menu: {menu.Name}");
		Console.WriteLine($"{menu.FoodItems.Count} food items");
		foreach (var item in menu.FoodItems)
		{
			Console.WriteLine($"Food Item: {item.Name} | {item.TimeToCook} | {item.Temperature}");
		}

		Console.ReadLine();
		
	}
}