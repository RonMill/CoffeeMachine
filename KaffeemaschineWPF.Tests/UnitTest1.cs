using KaffeemaschineWPF.Models;
using NUnit.Framework;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Tests
{
    public class Tests
    {
        private CoffeeMachine coffeeMachine;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

        }
        [SetUp]
        public void Setup()
        {
            coffeeMachine = new CoffeeMachine();
        }

        [TestCase(20, ExpectedResult = 2.5)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(0.5, ExpectedResult = 0.5)]
        public double FillWaterTest(double d)
        {
            coffeeMachine.FillWater(d).Wait();
            return coffeeMachine.Water;
        }

        [TestCase(15, ExpectedResult = 2.5)]
        [TestCase(2, ExpectedResult = 2)]
        [TestCase(0, ExpectedResult = 0)]
        public double FillBeansTest(double d)
        {
            coffeeMachine.FillBeans(d).Wait();
            return coffeeMachine.Beans;
        }

        [TestCase(5, 5, 2, CoffeeStrengthEnum.Mittel, ExpectedResult = CoffeeMessageEnum.Ok)]
        [TestCase(0.1, 5 ,2, CoffeeStrengthEnum.Mittel, ExpectedResult = CoffeeMessageEnum.WaterLow)]
        [TestCase(5, 0.1, 2, CoffeeStrengthEnum.Mittel, ExpectedResult = CoffeeMessageEnum.BeansLow)]
        [TestCase(2.5, 2.5, 10, CoffeeStrengthEnum.Mittel, ExpectedResult = CoffeeMessageEnum.AmountToHigh)]

        public CoffeeMessageEnum CalculateCoffeeTest(double amountWater, double amountBeans, double amount, CoffeeStrengthEnum coffeeStrength)
        {
            coffeeMachine.FillWater(amountWater).Wait();
            coffeeMachine.FillBeans(amountBeans).Wait();

           // return CoffeeMessage.Ok; //coffeeMachine.MakeCoffee(amountCoffee, coffeeStrength).Result;
            return coffeeMachine.Calculate(amount, coffeeStrength);
        }

        [TearDown]
        public void TearDown()
        {

        }
    }
}