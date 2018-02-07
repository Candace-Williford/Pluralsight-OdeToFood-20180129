using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;
using OdeToFood.ViewModels;

namespace OdeToFood.Controllers
{
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

        public IActionResult Create()
        {
            return View();
        }
    }
}