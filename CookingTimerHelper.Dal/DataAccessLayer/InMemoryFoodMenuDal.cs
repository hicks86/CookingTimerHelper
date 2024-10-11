using CookingTimerHelper.Dal.Dto;

namespace CookingTimerHelper.Dal.DataAccessLayer
{
    public class InMemoryFoodMenuRepository : IFoodMenuDal
    {
        private static Dictionary<MenuDto, List<FoodItemDto>> database;

        public InMemoryFoodMenuRepository()
        {
            if (database == null)
            {
                database = new Dictionary<MenuDto, List<FoodItemDto>>();
            }
        }

        // Get all FoodItemDtos for a specific Menu
        public List<FoodItemDto> GetFoodItems(MenuDto menu)
        {
            if (database.TryGetValue(menu, out var foodItems))
            {
                return foodItems;
            }

            return new List<FoodItemDto>();
        }

        // Get a specific FoodItem by Id
        public FoodItemDto GetFoodItem(MenuDto menu, int foodItemId)
        {
            if (database.TryGetValue(menu, out var foodItems))
            {
                return foodItems.FirstOrDefault(f => f.Id == foodItemId);
            }

            return null;
        }

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
