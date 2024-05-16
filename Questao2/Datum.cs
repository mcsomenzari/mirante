using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Questao2
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Datum
    {
        [JsonPropertyName("competition")]
        public string Competition { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("round")]
        public string Round { get; set; }

        [JsonPropertyName("team1")]
        public string Team1 { get; set; }

        [JsonPropertyName("team2")]
        public string Team2 { get; set; }

        [JsonPropertyName("team1goals")]
        public string Team1goals { get; set; }

        [JsonPropertyName("team2goals")]
        public string Team2goals { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int Per_page { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int Total_pages { get; set; }

        [JsonPropertyName("data")]
        public List<Datum>? Data { get; set; }
    }




}
