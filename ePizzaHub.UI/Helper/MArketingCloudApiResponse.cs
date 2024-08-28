using Newtonsoft.Json;

namespace ePizzaHub.UI.Helper
{
    public class MArketingCloudApiResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
