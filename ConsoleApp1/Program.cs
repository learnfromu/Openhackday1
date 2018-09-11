using System;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var databaseId = "Openhackdb";
            var collectionId = "Ratings";
            var endpointUrl = "https://ch2db.documents.azure.com:443/";
            var authorizationKey = "PKtozp4b4GKoI59psRXTSw6vXm6jDTIdcyVfBxIkB52x9dDTQFW6N6Vy9Cwz8jl3cV7nse5owLCS0glrkGGXtw==";
            var client = new DocumentClient(new Uri(endpointUrl), authorizationKey);
            var collectionLink = UriFactory.CreateDocumentCollectionUri(databaseId, collectionId);
            var product = new { ProductId = "aa", UserId = "bbb", Ratings = 5 };
            Document created = client.CreateDocumentAsync(collectionLink, product).Result;
            Console.WriteLine(created);

        }
    }
}
