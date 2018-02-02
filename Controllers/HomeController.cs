using Microsoft.AspNetCore.Mvc;
using OdeToFood.Services;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller //receives request for root of the application
    {
        private IRestaurantData _restaurantData;

        public HomeController(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }

        //IActionResult will allow you to return different results
        //ActionResult doesn't immediately immediately write to the response. returns data struction that tells .NET Core where to go next in the pipleline
        //particularly handy for unit testing because it allows you to check what's returned and how it behaves without having to set up a full web server
        public IActionResult Index() //checks for this method
        {
            //return Content("Hello from the home controller.");
            var model = _restaurantData.GetAll();

            //return new ObjectResult(model); //controller doesn't care how this is returned in the response be it JSON, XML, or whatever. That actionresult is implemented later in the MVC framework
            return View(model); //no param, it defaults to using the method or action name as the view name.
        }
    }
}