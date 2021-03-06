﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcFilterDemo.Filters;

namespace MvcFilterDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [MyAuthorizeFilter]
    [MyActionFilter1(Order =2)]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        //[MyActionFilter1]
        //[MyActionFilter]
        [MyActionFilter2(Order =1)]
        [MyFilterFactoryAttribute(typeof(MyNoAttributeActionFilter))]
        public IEnumerable<WeatherForecast> Get()
        {

            Console.WriteLine("======Action方法执行=======");
            //throw new Exception("Action抛出异常了");
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
