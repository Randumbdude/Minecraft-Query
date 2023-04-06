using Newtonsoft.Json;

namespace Console_App
{
    public class ServerStatus
    {
        // Don't care about description, this changes over different server

        [JsonProperty("players")]
        public Players Players { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("favicon")]
        public string Icon { get; set; }
    }

    public class Players
    {
        [JsonProperty("max")]
        public long Max { get; set; }

        [JsonProperty("online")]
        public long Online { get; set; }

        /*
        [JsonProperty("sample")]
        public Sample Sample { get; set; }
        */
    }


    public class Sample
    {
        [JsonProperty("id")]
        public string[] Id { get; set; }

        [JsonProperty("name")]
        public string[] Name { get; set; }
    }


    public class Version
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("protocol")]
        public long Protocol { get; set; }
    }

    public class Description
    {
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
