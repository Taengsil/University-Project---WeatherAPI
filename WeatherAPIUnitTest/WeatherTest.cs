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
            string initialCityName = "Chicago";
            string initialStateName = "IL";
            string expectedCityName = "Chicago";
            string expectedStateCode = "US-IL";
            string[] arguments = { expectedCityName, initialStateName };


            WeatherAPI.Program.ReadInput(arguments, ref initialCityName, ref initialStateName);

            Assert.AreEqual(expectedCityName, initialCityName);
            Assert.AreEqual(expectedStateCode, initialStateName);
        }

        [TestMethod]
        public async Task testMainWithInvalidData()
        {
            string expectedCityName = "Chicoga";
            string initialStateName = "AR";
            string[] arguments = { expectedCityName, initialStateName };
            bool expectedValue = false;


            using (var sw = new StringWriter())
            {
                await Program.Main(arguments);

                var result = true;
                Assert.AreEqual(expectedValue, result);
            }
            
        }
    }
}