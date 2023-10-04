using Client.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITokenService _tokenService;

        public HomeController(
            ITokenService tokenService,
            ILogger<HomeController> logger)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            
                var data = new List<WeatherData>();

                using (var client = new HttpClient())
                {
                    //var tokenResponse = await _tokenService.GetToken("weatherapi.read");

                    //client
                    //  .SetBearerToken(tokenResponse.AccessToken);

                    var result = client
                      .GetAsync("https://localhost:7216/WeatherForecast")
                      .Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var model = result.Content.ReadAsStringAsync().Result;

                        data = JsonConvert.DeserializeObject<List<WeatherData>>(model);

                        return View(data);
                    }
                    else
                    {
                        throw new Exception("Unable to get content");
                    }

                }
            
        }
            

            public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}