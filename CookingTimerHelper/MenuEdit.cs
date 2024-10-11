using CookingTimerHelper.Biz;
using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.Dto;
using Csla;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CookingTimerHelper
{
    [Serializable]
    public class MenuEdit : BusinessBase<MenuEdit>
    {
        #region Business Methods

        // TODO: add your own fields, properties and methods

        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
        [Required]
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<FoodItemChild> CurrentFoodItemProperty = RegisterProperty<FoodItemChild>(nameof(CurrentFoodItem));
        public FoodItemChild CurrentFoodItem
        {
            get => GetProperty(CurrentFoodItemProperty);
            set => SetProperty(CurrentFoodItemProperty, value);
        }

        public static readonly PropertyInfo<MenuRootList> FoodItemsProperty = RegisterProperty<MenuRootList>(nameof(FoodItems));
        public MenuRootList FoodItems
        {
            get { return GetProperty(FoodItemsProperty); }
            set { SetProperty(FoodItemsProperty, value); }
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            base.AddBusinessRules();

            //BusinessRules.AddRule(new Rule(IdProperty));
        }

        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [ObjectAuthorizationRules]
        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion
        
        #region Data Access

        [Create]
        protected void Create([Inject]IChildDataPortal<MenuRootList> childDataPortal, [Inject] IChildDataPortal<FoodItemChild> foodItemchildDataPortal)
        {
            Id = -1;
            Name = "";
            FoodItems = childDataPortal.CreateChild();
            CurrentFoodItem = foodItemchildDataPortal.CreateChild();
            base.Child_Create();
        }

        [Insert]
        protected void Insert([Inject]IFoodMenuDal dal)
        {
            var dto = new MenuDto
            {
                Id = Id,
                Name = Name
            };

            Id = dal.AddMenu(dto);
        }

        [InsertChild]
        private void InsertChild([Inject] IFoodMenuDal dal, [Inject] IChildDataPortal<MenuRootList> childDataPortal)
        {
            FoodItems.Add(CurrentFoodItem);
        }


        [UpdateChild]
        private void UpdateChild([Inject] IFoodMenuDal dal, [Inject]IChildDataPortal<MenuRootList> childDataPortal)
        {

        }

        [Update]
        protected void Update([Inject]IFoodMenuDal dal)
        {
            //var menu = new MenuDto { Id = Id, Name = Name };

            //foreach (FoodItemChild item in FoodItems)
            //{
            //    var foodItem = new FoodItemDto
            //    {
            //        Name = item.Name,
            //        TimeToCook = item.TimeToCook,
            //        PreparationTime = item.PreparationTime,
            //        Device = item.Device,
            //        Temperature = item.Temperature,
            //        TemperatureUnit = TemperatureUnitEnum.Celius
            //    };

            //    dal.AddFoodItem(menu, foodItem);
            //}
        }

        [Fetch]
        private void Fetch([Inject]IFoodMenuDal menuDal, int id)
        {
            var data = menuDal.GetMenu(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
            }

        }

        #endregion
    }
}
