using KaffeemaschineWPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.APIService
{
    public class CoffeeAPI
    {
        private readonly HttpClient _httpClient;

        public CoffeeAPI()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://www.quandl.com/api/v3/datasets/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }
        public async Task<double> GetCoffeePrice(CoffeeBrand coffeeBrand)
        {
            string description = GetEnumDescription(coffeeBrand);
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync("ODA/" + description + "?api_key=EN3Epzvu9P6AVvcTPJTV");
            httpResponseMessage.EnsureSuccessStatusCode();
            string coffeestring = await httpResponseMessage.Content.ReadAsStringAsync();
            CoffeeAPIData coffeeAPIData = JsonConvert.DeserializeObject<CoffeeAPIData>(coffeestring);
            double price = Convert.ToDouble(coffeeAPIData.Dataset.Data[0][1]);
            return price;
        }
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

    }
}
