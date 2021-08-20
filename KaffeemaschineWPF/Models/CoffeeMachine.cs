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
        private double _water;
        private double _beans;
        private double _totalAmount;
        private readonly MediaManager mediaManager;

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
        public CoffeeMachine()
        {
            mediaManager = new MediaManager();
            MaxWater = 2.5;
            MaxBeans = 2.5;
            Water = 0;
            Beans = 0;
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
        public async Task<CoffeeMessage> MakeCoffee(double amount, CoffeeStrength coffeeStrength)
        {
            await mediaManager.LoadSound(CoffeeMachineTasks.MakeEspresso);
            double ratioBeansToWater = GetRatioForCoffeeStrength(coffeeStrength);
            double ratioWaterToBeans = 1 - ratioBeansToWater;

            return Calculate(amount, ratioBeansToWater, ratioWaterToBeans);
        }

        private CoffeeMessage Calculate(double amount, double ratioBeans, double ratioWater)
        {
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
