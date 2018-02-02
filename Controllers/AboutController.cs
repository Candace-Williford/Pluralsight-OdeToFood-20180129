using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    //[Route("[controller]")] // [controller] = token that corresponds to the name of the controller
    [Route("company/[controller]/[action]")]
    public class AboutController //receives request for root of the application
    {
        //[Route("")]
        public string Phone()
        {
            return "1+555+555+5555";
        }

        //[Route("[action]")] //[action] = token that corresponds to the name of the action/method
        public string Address()
        {
            return "USA";
        }
    }

}

//controller gets something then use services to build it out to a model
//that model can then be sent as a response from a REST API
//or it can be sent to a view to be rendered as HTML