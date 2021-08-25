using KaffeemaschineWPF.APIService;
using KaffeemaschineWPF.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Tests
{
    public class CoffeeAPITest
    {
        private CoffeeAPI coffeeAPI;
        [SetUp]
        public void Setup()
        {
            coffeeAPI = new CoffeeAPI();
        }

        [TestCase(CoffeeBrand.Arabica,ExpectedResult = 208.07788651128985)]
        [TestCase(CoffeeBrand.Robusta,ExpectedResult = 109.55561229243003)]
        public double GetCoffeeDataTest(CoffeeBrand coffeeBrand)
        {
            double price = coffeeAPI.GetCoffeePrice(coffeeBrand).Result;
            return price;
        }
    }
}
