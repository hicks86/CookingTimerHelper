using Csla;
using System;

namespace CookingTimerHelper.Biz
{
    [Serializable]
    public class FoodItemChild : BusinessBase<FoodItemChild>
    {
        #region Business Methods

        // example with private backing field
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id, RelationshipTypes.PrivateField);
        private int _Id = IdProperty.DefaultValue;
        public int Id
        {
            get { return GetProperty(IdProperty, _Id); }
            set { SetProperty(IdProperty, ref _Id, value); }
        }

        // example with managed backing field
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        public static readonly PropertyInfo<TimeSpan> TimeToCookProperty = RegisterProperty<TimeSpan>(nameof(TimeToCook));
        public TimeSpan TimeToCook
        {
            get => GetProperty(TimeToCookProperty);
            set => SetProperty(TimeToCookProperty, value);
        }

        public static readonly PropertyInfo<TimeSpan> PreparationTimeProperty = RegisterProperty<TimeSpan>(nameof(PreparationTime));
        public TimeSpan PreparationTime
        {
            get => GetProperty(PreparationTimeProperty);
            set => SetProperty(PreparationTimeProperty, value);
        }

        public static readonly PropertyInfo<string> DeviceProperty = RegisterProperty<string>(nameof(Device));
        public string Device
        {
            get => GetProperty(DeviceProperty);
            set => SetProperty(DeviceProperty, value);
        }

        public static readonly PropertyInfo<int> TemperatureProperty = RegisterProperty<int>(nameof(Temperature));
        public int Temperature
        {
            get => GetProperty(TemperatureProperty);
            set => SetProperty(TemperatureProperty, value);
        }



        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // TODO: add validation rules
            //BusinessRules.AddRule(new Rule(), IdProperty);
        }

        private static void AddObjectAuthorizationRules()
        {
            // TODO: add authorization rules
            //BusinessRules.AddRule(...);
        }

        #endregion

        #region Data Access

        [CreateChild]
        private void CreateChild()
        {
            Name = string.Empty;
            TimeToCook = TimeSpan.Zero;
            Temperature = 0;
            PreparationTime = TimeSpan.Zero;
            Device = string.Empty;
            base.Child_Create();
        }

        //[Create]
        //protected void Create(object criteria)
        //{
        //    var split = ((string)criteria).Split();
        //    // TODO: load default values
        //    // omit this override if you have no defaults to set
        //    base.Child_Create();
        //    MarkAsChild();
        //}

        #endregion
    }
}