using CookingTimerHelper.Dal.Dto;

namespace CookingTimerHelper.Dal
{
    public interface IFoodMenuDal
    {
        int AddMenu(MenuDto menu);
        MenuDto GetMenu(int id);
        List<MenuDto> GetAllMenuItems();
        
        List<FoodItemDto> GetFoodItems(MenuDto menu);
        FoodItemDto GetFoodItem(MenuDto menu, int foodItemId);
        void AddFoodItem(MenuDto menu, FoodItemDto foodItem);
        void UpdateFoodItem(MenuDto menu, FoodItemDto foodItem);
        void DeleteFoodItem(MenuDto menu, int foodItemId);
    }
}
