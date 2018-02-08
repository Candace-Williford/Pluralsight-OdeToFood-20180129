using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        
        [Display(Name="Restaurant Name")]
        [Required, MaxLength(80)]
        public string Name { get; set; }
        public CuisineType Cuisine { get; set; }
    }

}

//Entity model
    //interacts with the DB
//view model (DTO or Data Transfer Obect)
    //interacts with a view
    //usually receives a list of entity models that are used to populate the view
//view models are frequently used to carry data into and out of an entity