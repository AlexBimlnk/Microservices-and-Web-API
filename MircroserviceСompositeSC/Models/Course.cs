using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MircroserviceСompositeSC.Models
{
    [Serializable]
    public class Course
    {
        [JsonPropertyName("id")]
        public long Id { get; set ; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("disciplenes")]
        public string Disciplenes { get; set; }
    }
}
