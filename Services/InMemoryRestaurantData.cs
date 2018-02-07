using System.Collections.Generic;
using System.Linq;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Candace's Pizza Place" },
                new Restaurant { Id = 2, Name = "Tersiguel's" },
                new Restaurant { Id = 3, Name = "King's Contrivances" }
            };
        }

        IEnumerable<Restaurant> IRestaurantData.GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        List<Restaurant> _restaurants;
    }
}