using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HellowWord.DTO
{
    public class AccountDTO
    {
        [JsonPropertyName("accountId")]
        public string AccountId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address1_Line1")]
        public string Address1_Line1 { get; set; }

        [JsonPropertyName("primarycontactid")]
        public LookUpEntityReferenceDTO PrimaryContactId { get; set; }
    }
}
