using KaffeemaschineWPF.APIService;
using KaffeemaschineWPF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KaffeemaschineWPF.Models
{
    public class CoffeeMachine : ObservableObject
    {
        private const double PROFIT = 0.8;
        private double _water;
        private double _beans;
        private double _totalAmount;
        private double _priceToPay;
        private readonly MediaManager mediaManager;
        private CoffeeAPI coffeeAPI;

        public double MaxWater { get; }
        public double MaxBeans { get; }
        public double Water
        {
            get => _water;
            set => SetProperty(ref _water, value);
        }
        public double Beans
        {
            get => _beans;
            set => SetProperty(ref _beans, value);
        }
        public double TotalAmount
        {
            get => _totalAmount;
            set => SetProperty(ref _totalAmount, value);
        }
        public double PriceToPay
        {
            get => _priceToPay;
            set => SetProperty(ref _priceToPay, value);
        }

        public CoffeeMachine()
        {
            mediaManager = new MediaManager();
            coffeeAPI = new CoffeeAPI();
            MaxWater = 2.5;
            MaxBeans = 2.5;
            Water = 0;
            Beans = 0;
        }

        public async void GetPriceToPay(CoffeeBrand coffeeBrand, double amount)
        {
            double price = await coffeeAPI.GetCoffeePrice(coffeeBrand);
            price = Math.Round(price / 100 * amount * PROFIT, 2);
            PriceToPay = price * 0.85;
        }
        public async Task FillWater(double amount)
        {

            await mediaManager.LoadSound(CoffeeMachineTasks.FillWater);
            if (Water + amount <= MaxWater)
            {
                Water += amount;
                return;
            }
            Water = MaxWater;
        }
        public async Task FillBeans(double amount)
        {
            await mediaManager.LoadSound(CoffeeMachineTasks.FillBeans);
            if (Beans + amount <= MaxBeans)
            {
                Beans += amount;
                return;
            }
            Beans = MaxBeans;
        }
        private double GetRatioForCoffeeStrength(CoffeeStrength coffeeStrength)
        {
            switch (coffeeStrength)
            {
                case CoffeeStrength.Stark: return 0.66;

                case CoffeeStrength.Mittel: return 0.5;

                case CoffeeStrength.Schwach: return 0.33;

                default: return 0;

            }
        }
        public async Task PrepareCoffee()
        {
            await mediaManager.LoadSound(CoffeeMachineTasks.GrindCoffee);
            await mediaManager.LoadSound(CoffeeMachineTasks.Pump);
        }
        public async Task MakeCoffeeSound(/*double amount, CoffeeStrength coffeeStrength*/)
        {
            await mediaManager.LoadSound(CoffeeMachineTasks.MakeEspresso);
        }

        public CoffeeMessage Calculate(double amount, CoffeeStrength coffeeStrength)
        {
            double ratioBeans = GetRatioForCoffeeStrength(coffeeStrength);
            double ratioWater = 1 - ratioBeans;

            double water = amount * ratioWater;
            double beans = amount * ratioBeans;

            if (amount * ratioBeans > MaxBeans || amount * ratioWater > MaxWater)
                return CoffeeMessage.AmountToHigh;
            if (water > Water)
                return CoffeeMessage.WaterLow;
            if (beans > Beans)
                return CoffeeMessage.BeansLow;

            Water -= water;
            Beans -= beans;
            TotalAmount += amount;
            return CoffeeMessage.Ok;
        }
    }
}
