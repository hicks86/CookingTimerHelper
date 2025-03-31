using CookSync.Dal;
using Csla;

namespace CookSync.Biz
{
    [Serializable]
    public class ListOfMenus : ReadOnlyListBase<ListOfMenus, MenuInfo>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(ListOfMenus), "Role");
        }

        #endregion

        #region Data Access

        [Fetch]
        private void Fetch([Inject] IFoodMenuDal menuDal, [Inject] IChildDataPortal<MenuInfo> menuInfoPortal)
        {
            using (LoadListMode)
            {
                var data = menuDal.GetAllMenuItems()
                                  .Select(menu => (menu, menuDal.GetFoodItems(menu.Id).Count))
                                  .Select(val => menuInfoPortal.FetchChild((val.menu, val.Count)));

                AddRange(data);
            }
        }

        #endregion
    }
}
