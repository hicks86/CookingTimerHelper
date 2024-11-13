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

        public static readonly PropertyInfo<MenuFood> FoodItemsProperty = RegisterProperty<MenuFood>(nameof(FoodItems));
        public MenuFood FoodItems
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
        protected void Create([Inject]IChildDataPortal<MenuFood> childDataPortal, [Inject] IChildDataPortal<FoodItemEdit> foodItemchildDataPortal)
        {
            Id = -1;
            Name = "";
            FoodItems = childDataPortal.CreateChild();
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

		[Update]
        protected void Update([Inject]IFoodMenuDal dal)
        {
            var menu = new MenuDto { Id = Id, Name = Name };

            foreach (var item in FoodItems)
            {
                var foodItem = new FoodItemDto
                {
                    Name = item.Name,
                    TimeToCook = item.TimeToCook,
                    PreparationTime = item.PreparationTime,
                    Device = item.Device,
                    Temperature = item.Temperature,
                    TemperatureUnit = TemperatureUnitEnum.Celius
                };

                dal.AddFoodItem(menu, foodItem);
            }
        }

        [Fetch]
        private void Fetch([Inject]IFoodMenuDal menuDal, [Inject]IChildDataPortal<MenuFood> menuFoodDataPortal, [Inject]IChildDataPortal<FoodItemEdit> foodItemDataPortal, int id)
        {
            var data = menuDal.GetMenu(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
                FoodItems = menuFoodDataPortal.FetchChild(data.Id);
            }

        }

        #endregion
    }
}
