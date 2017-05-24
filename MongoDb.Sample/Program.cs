using MongoDb.Sample.Model;
using System;
using System.Threading.Tasks;

namespace MongoDb.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MongoDb Client .NET Core DEMO");

            AddDocumentTest().Wait();

            GetDocumentsTest().Wait();

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }

        private static async Task GetDocumentsTest()
        {
            var settings = new Settings { ConnectionString = "mongodb://localhost:27017", Database = "TestDb" };

            IMeasuresService measuresService = new MeasuresService(settings);

            var documents = await measuresService.Get();

            foreach (var item in documents)
            {
                Console.WriteLine(item);
            }

        }

        private static async Task AddDocumentTest()
        {
            
            var settings = new Settings { ConnectionString = "mongodb://localhost:27017", Database = "TestDb" };

            IMeasuresService measuresService = new MeasuresService(settings);

            for (int i = 0; i < 1000; i++)
            {
                string json = "{'deviceId':'dev-001', 'temperature':24.72, 'pressure':1021.04, 'voltage':4.99, 'current':15.04}";

                Console.WriteLine($"Inserting document: {json}");
                await measuresService.Add(json);

                Console.WriteLine("Added.");
            }
            

        }
    }
}