using CookingTimerHelper.Dal;
using CookingTimerHelper.Dal.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingTimerHelper.DataAccessLayer
{
    public class InMemoryFoodMenuDal : IFoodMenuDal
    {
        // this is our in-memory database
        private static List<FoodItemDto> list = new List<FoodItemDto>();

        public FoodItemDto Create()
        {
            return new FoodItemDto { Id = -1 };
        }

        public FoodItemDto GetMenuItem(int id)
        {
            var entity = list.FirstOrDefault(_ => _.Id == id);
            if (entity == null)
                throw new Exception("Index not found");
            return entity;
        }

        public int InsertMenuItem(FoodItemDto data)
        {
            var newId = 1;
            if (list.Count > 0)
                newId = list.Max(_ => _.Id) + 1;
            data.Id = newId;
            list.Add(data);
            return newId;
        }

        public void UpdateMenuItem(FoodItemDto data)
        {
            var entity = GetMenuItem(data.Id);
            entity.Name = data.Name;
            entity.TimeToCook = data.TimeToCook;
            entity.PreparationTime = data.PreparationTime;
            entity.Device = data.Device;
            entity.Temperature = data.Temperature;
            entity.TemperatureUnit = data.TemperatureUnit;
        }

        public void DeleteMenuItem(int id)
        {
            var entity = GetMenuItem(id);
            list.Remove(entity);
        }

    }
}
