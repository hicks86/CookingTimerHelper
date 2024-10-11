using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.Dto;
using Csla;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CookingTimerHelper.Biz
{
    [Serializable]
    public class MenuRootList : BusinessListBase<MenuRootList, FoodItemChild>
    {
        #region Authorization Rules

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //AuthorizationRules.AllowGet(typeof(MenuRootList), "Role");
        }

        #endregion

        [CreateChild]
        private void CreateChild()
        {
            base.Child_Create();
        }
    }

    
}
