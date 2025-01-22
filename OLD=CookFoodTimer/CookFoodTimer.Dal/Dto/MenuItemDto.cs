using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingTimerHelper.Dal.Dto
{
    public class MenuDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class FoodItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TimeSpan? TimeToCook { get; set; }

        public TimeSpan? PreparationTime { get; set; }

        public string Device { get; set; }

        public double? Temperature { get; set; }

        public TemperatureUnitEnum TemperatureUnit { get; set; }
    }
}
