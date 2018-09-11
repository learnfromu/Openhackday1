
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["productId"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            var databaseId = "Openhackdb";
            var collectionId = "Ratings";
            var endpointUrl = "https://ch2db.documents.azure.com:443/";
            var authorizationKey = "PKtozp4b4GKoI59psRXTSw6vXm6jDTIdcyVfBxIkB52x9dDTQFW6N6Vy9Cwz8jl3cV7nse5owLCS0glrkGGXtw==";
            var client = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
            var product = new { ProductId = "aa", UserId = "bbb", Ratings = 5 };
            Document created = client.CreateDocumentAsync(collectionLink, product).Result;
            Console.WriteLine(created);
            return name != null
                ? (ActionResult)new OkObjectResult($"The product name for your product id: {name} is Starfruit Explosion")
                : new BadRequestObjectResult("Please pass a productId on the query string or in the request body");
        }
    }
}
