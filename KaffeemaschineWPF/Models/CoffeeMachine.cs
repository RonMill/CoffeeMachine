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
            await mediaManager.LoadSound(CoffeeMachineTasksEnum.FillWater);
            if (Water + amount <= MaxWater)
            {
                Water += amount;
                return;
            }
            Water = MaxWater;
        }
        public async Task FillBeans(double amount)
        {
            await mediaManager.LoadSound(CoffeeMachineTasksEnum.FillBeans);
            if (Beans + amount <= MaxBeans)
            {
                Beans += amount;
                return;
            }
            Beans = MaxBeans;
        }
        private double GetRatioForCoffeeStrength(CoffeeStrengthEnum coffeeStrength)
        {
            switch (coffeeStrength)
            {
                case CoffeeStrengthEnum.Stark: return 0.66;
                case CoffeeStrengthEnum.Mittel: return 0.5;
                case CoffeeStrengthEnum.Schwach: return 0.33;
                default: return 0;
            }
        }
        public async Task PrepareCoffee()
        {
            await mediaManager.LoadSound(CoffeeMachineTasksEnum.GrindCoffee);
            await mediaManager.LoadSound(CoffeeMachineTasksEnum.Pump);
        }
        public async Task MakeCoffeeSound(/*double amount, CoffeeStrength coffeeStrength*/)
        {
            await mediaManager.LoadSound(CoffeeMachineTasksEnum.MakeEspresso);
        }

        public CoffeeMessageEnum Calculate(double amount, CoffeeStrengthEnum coffeeStrength)
        {
            double ratioBeans = GetRatioForCoffeeStrength(coffeeStrength);
            double ratioWater = 1 - ratioBeans;

            double water = amount * ratioWater;
            double beans = amount * ratioBeans;

            if (amount * ratioBeans > MaxBeans || amount * ratioWater > MaxWater)
                return CoffeeMessageEnum.AmountToHigh;
            if (water > Water)
                return CoffeeMessageEnum.WaterLow;
            if (beans > Beans)
                return CoffeeMessageEnum.BeansLow;
            Water -= water;
            Beans -= beans;
            TotalAmount += amount;
            return CoffeeMessageEnum.Ok;
        }
    }
}