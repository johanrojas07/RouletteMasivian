using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RouletteApi.Tests
{
    [TestClass]
    public class ServicioRouletteTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var rouletteController = new RouletteController();
            List<RouletteDto> datos = rouletteController.Get() as ViewResult;
            Assert.IsNotNull(datos);
        }
    }
}
