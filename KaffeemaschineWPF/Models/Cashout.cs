using KaffeemaschineWPF.APIService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.Models
{
    public class Cashout
    {
        private const double PROFIT = 0.8;
        private readonly CoffeeAPI coffeeAPI;
        private readonly CurrencyAPI currencyAPI;
        public Cashout()
        {
            coffeeAPI = new CoffeeAPI();
            currencyAPI = new CurrencyAPI();
        }
        public async Task<double> GetPriceToPay(CoffeeBrandEnum coffeeBrand, double amount)
        {
            double coffeePrice = await coffeeAPI.GetCoffeePrice(coffeeBrand);
            coffeePrice = Math.Round(coffeePrice / 100 * amount * PROFIT, 2);
            double exchangeRate = await currencyAPI.GetCurrencyExchangeRateUSDEUR();
            return coffeePrice / exchangeRate;
        }
    }
}