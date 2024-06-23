using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheBikeSupplier.Models.Client
{
    public class ProblemDetails
    {
        [JsonPropertyName("detail")]
        public string Detail { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }
    }
}
