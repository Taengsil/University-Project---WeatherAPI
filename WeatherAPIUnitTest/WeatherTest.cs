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
        //To do: Add mocks and detailed unit tests

        [TestMethod]
        public void testReadInput()
        {
            string initialCityName = "Chicago";
            string initialStateName = "IL";
            string expectedCityName = "Chicago";
            string expectedStateCode = "US-IL";
            string[] arguments = { expectedCityName, initialStateName };


            WeatherProcessor.ReadInput(arguments, ref initialCityName, ref initialStateName);

            Assert.AreEqual(expectedCityName, initialCityName);
            Assert.AreEqual(expectedStateCode, initialStateName);
        }
    }
}
