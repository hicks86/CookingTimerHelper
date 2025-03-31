using CookSync.Dal;
using Csla;

namespace CookSync.Biz
{
    [Serializable]
    public class MenuFood : BusinessListBase<MenuFood, FoodItemEdit>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(MenuRootList), "Role");
        }

        #endregion

        [FetchChild]
        private void Fetch(int menuId, [Inject] IFoodMenuDal dal, [Inject] IChildDataPortal<FoodItemEdit> portal)
        {
            var data = dal.GetFoodItems(menuId);

            using (LoadListMode)
            {
                foreach (var item in data)
                {
                    Add(portal.FetchChild(item));
                }
            }
        }
    }


}
