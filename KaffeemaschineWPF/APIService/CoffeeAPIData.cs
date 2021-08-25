using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaffeemaschineWPF.APIService
{
    // CoffeeAPIData myDeserializedClass = JsonConvert.DeserializeObject<CoffeeAPIData>(myJsonResponse); 
    public class Dataset
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dataset_code")]
        public string DatasetCode { get; set; }

        [JsonProperty("database_code")]
        public string DatabaseCode { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("refreshed_at")]
        public string RefreshedAt { get; set; }

        [JsonProperty("newest_available_date")]
        public string NewestAvailableDate { get; set; }

        [JsonProperty("oldest_available_date")]
        public string OldestAvailableDate { get; set; }

        [JsonProperty("column_names")]
        public List<string> ColumnNames { get; set; }

        [JsonProperty("frequency")]
        public string Frequency { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("premium")]
        public bool Premium { get; set; }

        [JsonProperty("limit")]
        public object Limit { get; set; }

        [JsonProperty("transform")]
        public object Transform { get; set; }

        [JsonProperty("column_index")]
        public object ColumnIndex { get; set; }

        [JsonProperty("start_date")]
        public string StartDate { get; set; }

        [JsonProperty("end_date")]
        public string EndDate { get; set; }

        [JsonProperty("data")]
        public List<List<object>> Data { get; set; }

        [JsonProperty("collapse")]
        public object Collapse { get; set; }

        [JsonProperty("order")]
        public object Order { get; set; }

        [JsonProperty("database_id")]
        public int DatabaseId { get; set; }
    }

    public class CoffeeAPIData
    {
        [JsonProperty("dataset")]
        public Dataset Dataset { get; set; }
    }


}
