using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RouletteApi.Controllers;
using RouletteApi.Models;
using RouletteApi.Services;
using System.Collections.Generic;

namespace RouletteApiTests
{
    [TestClass]
    public class RouletteControllerTest
    {
        private readonly RouletteService _rouletteService;
        private readonly BetsService _betsService;
        private readonly IConfiguration Configuration;
        public RouletteControllerTest(RouletteService rouletteService, BetsService betsService, IConfiguration configuration)
        {
             _betsService = betsService;
            _rouletteService = rouletteService;
            Configuration = configuration;
        }


        [TestMethod]
        public void TestMethod1()
        {
            var rouletteController = new RouletteController(_rouletteService, _betsService, Configuration);
            ActionResult<List<RouletteDto>> datos = rouletteController.Get();
            Assert.IsNotNull(datos);
        }
    }
}
