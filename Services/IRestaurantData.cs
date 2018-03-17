using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant newRestaurant);
        Restaurant Update(Restaurant restaurant);
    }
}