using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HellowWord.DTO
{
    public class LookUpEntityReferenceDTO
    {
        [JsonPropertyName("id")]
        public Guid ID { get; set; }

        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("entityLogicalName")]
        public required string EntityLogicalName { get; set; }
    }
}
