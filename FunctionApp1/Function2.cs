
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;

namespace FunctionApp1
{
    public static class Function2
    {
        [FunctionName("GetProducts")]
        public static async Task<HttpResponseMessage> GetProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            string jsonResponseString = null;
            // parse query parameter
            using (var client = new HttpClient())
            {
                string url = "https://serverlessohproduct.trafficmanager.net/api/GetProducts";
                //var content = new StringContent(jsonParam, Encoding.UTF8, "application/json");
                var response = await client.GetAsync(url).ConfigureAwait(false);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var list = await response.Content.ReadAsAsync<Product[]>();                    
                }
                

            }

            //if (jsonResponseString != null)
            //{
            //    var products= JsonConvert.DeserializeObject<Product>(jsonResponseString);
            //    jsonResponseString = JsonConvert.SerializeObject(products);

            //}

            return jsonResponseString == null
            ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
            : req.CreateResponse(HttpStatusCode.OK, jsonResponseString);
        }
    }
}
