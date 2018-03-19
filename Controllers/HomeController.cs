using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
    //can place this attribute at controller OR action level
    //[Authorize] // just checks to make sure that the user is in fact authenicated. can also pass params that will check the users roles and policies. policies can contain and encapsulate all the checks you want to do on a user
    public class HomeController : Controller //receives request for root of the application
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;

        public HomeController(IRestaurantData restaurantData,
            IGreeter greeter)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
        }

        //IActionResult will allow you to return different results
        //ActionResult doesn't immediately immediately write to the response. returns data struction that tells .NET Core where to go next in the pipleline
        //particularly handy for unit testing because it allows you to check what's returned and how it behaves without having to set up a full web server
        //[AllowAnonymous] //overrides [Authorize] attribute on controller
        public IActionResult Index() //checks for this method
        {
            //return Content("Hello from the home controller.");
            //var model = _restaurantData.GetAll();
            var model = new HomeIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.CurrentMessage = _greeter.GetMessageOfTheDay();

            //return new ObjectResult(model); //controller doesn't care how this is returned in the response be it JSON, XML, or whatever. That actionresult is implemented later in the MVC framework
            return View(model); //no param, it defaults to using the method or action name as the view name.
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null) return RedirectToAction(nameof(Index)); //using nameof is handy if refactoring. sends back a string representation
            //return View("NotFound"); //best practice?
            //return NotFound(); is useful for APIs, returns HTTP status code 404
            return View(model);
        }

        [HttpGet] //this is a route constraint that will only be invoked if there's an HTTP GET request
        
        public IActionResult Create()
        {
            return View();
        }

        //The framework will try to set EVERY property on the Restaurant. malicious 
        //user can add additional posted form values to an HTTP request and try to 
        //manipulate info in the model that they don't have access to. 
        //known as overposting. can solve this vulnerability by using an input model
        //that only expects the properties that you would post from a form
        [HttpPost] //this is a route constraint that will only be invoked if there's an HTTP POST
        [ValidateAntiForgeryToken] //helps prevent cross-site request forgery by validating form token and confirming that's a form we actually sent to the user
        public IActionResult Create(RestaurantEditModel model) 
        {
            if(ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;

                newRestaurant = _restaurantData.Add(newRestaurant);

                //return View("Details", newRestaurant);
                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id }); //this ensures that the user doesn't stay on a page with a POST operation. You want to send back a redirect action instead that send a GET request instead
            }
            else
            {
                return View();
            }
        }
    }
}