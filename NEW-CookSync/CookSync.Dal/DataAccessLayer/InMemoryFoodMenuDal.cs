using CookSync.Dal.Dto;

namespace CookSync.Dal.DataAccessLayer
{
    public class InMemoryFoodMenuRepository : IFoodMenuDal
    {
        private static Dictionary<MenuDto, List<FoodItemDto>>? database;

        public InMemoryFoodMenuRepository()
        {
            if (database == null)
            {
                database = new Dictionary<MenuDto, List<FoodItemDto>>
                {
                    {
                        new MenuDto { Id = 1, Name = "Breakfast" },
                        [
                                new() {
                                    Id = 100,
                                    Name = "Scrambled Egg",
                                    Device = "Saucepan",
                                    PreparationTime = TimeSpan.FromMinutes(2),
                                    Temperature = null,
                                    TimeToCook = TimeSpan.FromMinutes(5),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },
                                new() {
                                    Id = 101,
                                    Name = "Pancakes",
                                    Device = "Frying Pan",
                                    PreparationTime = TimeSpan.FromMinutes(10),
                                    Temperature = 180, // Degrees in Celsius
                                    TimeToCook = TimeSpan.FromMinutes(8),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },

                                new() {
                                    Id = 102,
                                    Name = "Bacon",
                                    Device = "Grill",
                                    PreparationTime = TimeSpan.FromMinutes(2),
                                    Temperature = 200, // Degrees in Celsius
                                    TimeToCook = TimeSpan.FromMinutes(10),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },
                            ]
                    },
                    {
                        new MenuDto { Id = 2, Name = "Chicken Dinner" },
                        [
                                new FoodItemDto
                                {
                                    Id = 103,
                                    Name = "Grilled Chicken Breast",
                                    Device = "Grill",
                                    PreparationTime = TimeSpan.FromMinutes(5),
                                    Temperature = 180, // Degrees in Celsius
                                    TimeToCook = TimeSpan.FromMinutes(25),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },

                                new FoodItemDto
                                {
                                    Id = 104,
                                    Name = "Roasted Vegetables",
                                    Device = "Oven",
                                    PreparationTime = TimeSpan.FromMinutes(10),
                                    Temperature = 200, // Degrees in Celsius
                                    TimeToCook = TimeSpan.FromMinutes(30),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },

                                new FoodItemDto
                                {
                                    Id = 105,
                                    Name = "Steamed Broccoli",
                                    Device = "Steamer",
                                    PreparationTime = TimeSpan.FromMinutes(5),
                                    Temperature = null, // Steaming doesn't need specific temperature value
                                    TimeToCook = TimeSpan.FromMinutes(10),
                                    TemperatureUnit = TemperatureUnitEnum.Celsius
                                },
                            ]
                    }
                };
            }
        }

        // Get all FoodItemDtos for a specific Menu
        public List<FoodItemDto> GetFoodItems(int id)
        {
            return database
                    .Where(kvp => kvp.Key.Id == id)
                    .Select(kvp => kvp.Value)
                    .FirstOrDefault([]);
        }

        // Get a specific FoodItem by Id
        public FoodItemDto GetFoodItem(int menuId, int foodItemId)
            => GetFoodItems(menuId)
                    .Find(x => x.Id == foodItemId);

        // Add a FoodItem to a Menu
        public void AddFoodItem(MenuDto menu, FoodItemDto foodItem)
        {
            if (!database.ContainsKey(menu))
            {
                database[menu] = new List<FoodItemDto>();
            }

            foodItem.Id = database[menu].Any()
                        ? database[menu].Max(f => f.Id) + 1 : 1;  // Generate new ID
            database[menu].Add(foodItem);
        }

        // Update an existing FoodItem
        public void UpdateFoodItem(MenuDto menu, FoodItemDto foodItem)
        {
            if (database.TryGetValue(menu, out var foodItems))
            {
                var existingFoodItem = foodItems.Find(f => f.Id == foodItem.Id);
                if (existingFoodItem != null)
                {
                    existingFoodItem.Name = foodItem.Name;
                    existingFoodItem.TimeToCook = foodItem.TimeToCook;
                    existingFoodItem.PreparationTime = foodItem.PreparationTime;
                    existingFoodItem.Device = foodItem.Device;
                    existingFoodItem.Temperature = foodItem.Temperature;
                    existingFoodItem.TemperatureUnit = foodItem.TemperatureUnit;
                }
            }
        }

        // Delete a FoodItem by Id
        public void DeleteFoodItem(MenuDto menu, int foodItemId)
        {
            if (database.TryGetValue(menu, out var foodItems))
            {
                var foodItem = foodItems.FirstOrDefault(f => f.Id == foodItemId);
                if (foodItem != null)
                {
                    foodItems.Remove(foodItem);
                }
            }
        }

        public int AddMenu(MenuDto menu)
        {
            if (!database.ContainsKey(menu))
            {
                database[menu] = new List<FoodItemDto>();
                int position = new List<MenuDto>(database.Keys).IndexOf(menu) + 1;

                // Step 3: Update the MenuDto's Id property based on the position (if needed)
                menu.Id = position;
            }

            return menu.Id;
        }

        public void UpdateMenu(MenuDto menu)
        {
            if (database.Keys.Any(x => x.Id == menu.Id))
            {
                var existingMenu = database.Keys.FirstOrDefault(k => k.Id == menu.Id);
                if (existingMenu != null)
                {
                    existingMenu.Name = menu.Name;
                }
            }

        }

        public List<MenuDto> GetAllMenuItems()
        {
            return new List<MenuDto>(database.Keys);
        }

        public MenuDto GetMenu(int id)
        {
            return database.Keys.FirstOrDefault(x => x.Id == id);
        }
    }
}
