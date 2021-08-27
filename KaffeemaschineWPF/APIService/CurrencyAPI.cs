using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.APIService
{
    public class CurrencyAPI
    {
        private readonly HttpClient _httpClient;
        public CurrencyAPI()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://data.fixer.io/api/")
            };
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<double> GetCurrencyExchangeRateUSDEUR()
        {
            string apiKey = File.ReadAllText("APIKeys\\FixerKey.txt");
            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync($"latest?access_key={apiKey}");
            httpResponseMessage.EnsureSuccessStatusCode();
            string currencyString = await httpResponseMessage.Content.ReadAsStringAsync();
            CurrencyAPIData currencyAPIData = JsonConvert.DeserializeObject<CurrencyAPIData>(currencyString);
            return Convert.ToDouble(currencyAPIData.Rates.USD);
        }
    }
}