using CookSync.Dal.Dto;

namespace CookSync.Dal
{
    public interface IFoodMenuDal
    {
        int AddMenu(MenuDto menu);
        MenuDto GetMenu(int id);
        List<MenuDto> GetAllMenuItems();

        List<FoodItemDto> GetFoodItems(int menuId);
        FoodItemDto GetFoodItem(int menuId, int foodItemId);
        void AddFoodItem(MenuDto menu, FoodItemDto foodItem);
        void UpdateFoodItem(MenuDto menu, FoodItemDto foodItem);
        void DeleteFoodItem(MenuDto menu, int foodItemId);

        void UpdateMenu(MenuDto menu);
    }
}
