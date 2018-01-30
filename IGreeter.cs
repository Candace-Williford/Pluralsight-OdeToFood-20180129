using Microsoft.Extensions.Configuration;

namespace OdeToFood
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
    }

    public class Greeter : IGreeter
    {
        private IConfiguration _configuration;

        //IConfiguration service is registered by default by .NET Core
        public Greeter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        string IGreeter.GetMessageOfTheDay()
        {
            return _configuration["Greeting"];
        }
    }
}