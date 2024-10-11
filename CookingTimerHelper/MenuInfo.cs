using CookingTimerHelper.Dal.Dto;
using Csla;
using System;

namespace CookingTimerHelper
{
    [Serializable]
    public class MenuInfo : ReadOnlyBase<MenuInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public int Id
        {
            get { return GetProperty(IdProperty); }
            private set { LoadProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
        public string Name
        {
            get { return GetProperty(NameProperty); }
            private set { LoadProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<int> FoodItemCountProperty = RegisterProperty<int>(nameof(FoodItemCount));
        public int FoodItemCount
        {
            get => GetProperty(FoodItemCountProperty);
            private set => LoadProperty(FoodItemCountProperty, value);
        }

        #endregion


        #region Data Access

        [FetchChild]
        private void Fetch((MenuDto menu, int count) value)
        {
            Id = value.menu.Id;
            Name = value.menu.Name;
            FoodItemCount = value.count;
        }

        #endregion
    }
}
