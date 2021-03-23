using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAPI;
using System.Threading.Tasks;

namespace WeatherAPIUnitTest
{
    [TestClass]
    public class WeatherTest
    {

        [TestMethod]
        public void testReadInput()
        {
            string expectedCityName = "Chicago";
            string initialStateName = "IL";
            string expectedStateCode = "US-IL";
            string[] arguments = { expectedCityName, initialStateName };


            WeatherAPI.WeatherGeneratorFromAPI.ReadInput(arguments);

            Assert.AreEqual(expectedCityName, WeatherGeneratorFromAPI.CityName);
            Assert.AreEqual(expectedStateCode, WeatherGeneratorFromAPI.StateCode);
        }

        [TestMethod]
        public void testMainWithInvalidData()
        {
            string expectedCityName = "Chicoga";
            string initialStateName = "AR";
            string expectedStateCode = "US-AR";
            string[] arguments = { expectedCityName, initialStateName };
            bool expectedValue = false;


            using (var sw = new StringWriter())
            {
                Program.Main(arguments);
                Assert.AreEqual(expectedCityName, WeatherGeneratorFromAPI.CityName);
                Assert.AreEqual(expectedStateCode, WeatherGeneratorFromAPI.StateCode);

                var result = WeatherGeneratorFromAPI.inputIsCorrect;
                Assert.AreEqual(expectedValue, result);
            }
            
        }
    }
}