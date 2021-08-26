﻿using KaffeemaschineWPF.APIService;
using KaffeemaschineWPF.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Models
{
    public class Cashout : ObservableObject
    {
        private const double PROFIT = 0.8;
        private readonly CoffeeAPI coffeeAPI;
        private readonly CurrencyAPI currencyAPI;
        private double _priceToPay;

        public double PriceToPay
        {
            get => _priceToPay;
            set => SetProperty(ref _priceToPay, value);
        }
        public Cashout()
        {
            coffeeAPI = new CoffeeAPI();
            currencyAPI = new CurrencyAPI();
        }
        public async Task GetPriceToPay(CoffeeBrandEnum coffeeBrand, double amount)
        {
            double coffeePrice = await coffeeAPI.GetCoffeePrice(coffeeBrand);
            coffeePrice = Math.Round(coffeePrice / 100 * amount * PROFIT, 2);
            double exchangeRate = await currencyAPI.GetCurrencyExchangeRateUSDEUR();
            PriceToPay = coffeePrice / exchangeRate;
        }
    }
}