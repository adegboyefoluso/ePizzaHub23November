using ePizzaHub.Models;
using Newtonsoft.Json;
using System.Text;

namespace ePizzaHub.UI.Helper
{
    public static class Marketingcloud
    {

        private const string clientId = "es7pk0nh3rt8djmy26m5zdd1";
        private const string clientSecret = "okCwTxEHGT40LiAU5f8JEsGC";
        private const string grantType = "client_credentials";
        private const string authEndpoint = "https://mc8dwj4pq2qdw5ycdpk2bk8fgmn8.auth.marketingcloudapis.com/v2/token";

        public static  async Task<string> GetAccessTokenAsync()
        {
            // var config = _configuration.GetSection("MCCredentials").Get<MCCredentials>();
            try
            {
                using (var client = new HttpClient())
                {
                    var requestContent = new StringContent($"grant_type={grantType}&client_id={clientId}&client_secret={clientSecret}",
                        Encoding.UTF8, "application/x-www-form-urlencoded");
                    //var requestContent = new StringContent($"grant_type={grantType}&client_id={clientId}&client_secret={clientSecret}",
                    //   Encoding.UTF8, "application/x-www-form-urlencoded");

                    var response = await client.PostAsync(authEndpoint, requestContent);
                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    var apiResponse = JsonConvert.DeserializeObject<MArketingCloudApiResponse>(responseContent);

                    return apiResponse.AccessToken;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error authenticating with Marketing Cloud: {ex.Message}");
                return null;
            }
        }

        public static  bool LowLatencyTriggersend(UserModel model)
        {

            string accessToken = GetAccessTokenAsync().Result;
            HttpResponseMessage respo;

            using (HttpClient client = new HttpClient())
            {
                var guid = Guid.NewGuid().ToString();
                var messagekey = model.Email + "_" + guid;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                var endpoint = $"https://mc8dwj4pq2qdw5ycdpk2bk8fgmn8.rest.marketingcloudapis.com/messaging/v1/email/messages/{messagekey}";

                var data = new Transactional
                {
                    definitionKey = "MyTransactional",
                    recipient = new TranRecipient
                    {
                        contactKey = model.Id.ToString(),
                        to = model.Email,
                        attributes = new AttributesClass()
                        {
                            EmailAddress = model.Email,
                            Subscriberkey = model.Id.ToString(),
                            MessageKey = messagekey
                        }
                    }


                };
                var result = JsonConvert.SerializeObject(data);
                var obj = new StringContent(result, Encoding.UTF8, "application/json");
                respo = client.PostAsync(endpoint, obj).Result;
                var IsSucces = respo.EnsureSuccessStatusCode().IsSuccessStatusCode;

                if (IsSucces)
                {
                    return true;
                }
                else { return false; }
            }
        }

    }
}

