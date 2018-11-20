using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonConvertSerializaObject
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueMessage mesg = new QueueMessage
            {
                Vin = "test",
                CampaignRef = 12,
                RecallId = "23",
                Type = QueueMessageType.Recall,
                Brand = "b1",
                Version = 1
            };
            string mes = JsonConvert.SerializeObject(mesg);
        }
    }

    public class QueueMessage
    {
        [JsonProperty("vin")] public string Vin { get; set; }

        [JsonProperty("recallId")] public string RecallId { get; set; }

        [JsonProperty("campaignRef")] public int CampaignRef { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public QueueMessageType Type { get; set; }

        [JsonProperty("brand")] public string Brand { get; set; }

        [JsonProperty("version")] public int Version { get; set; }
    }

    public enum QueueMessageType
    {
        Recall,
        ThankYou
    }
}
