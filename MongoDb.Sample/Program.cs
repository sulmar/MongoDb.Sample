using Microsoft.Extensions.Configuration;
using MongoDb.Sample.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MongoDb.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MongoDb Client .NET Core DEMO");

            var connectionString = GetConnectionString();

            //AddDocumentTest().Wait();

            //GetDocumentsTest().Wait();

            Task.Run(() => SendDeviceToMongoDbMessagesAsync(connectionString));

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

        }

        private static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");

            IConfigurationRoot Configuration = builder.Build();

            var connectionString = Configuration.GetConnectionString("MyDatabase");

            return connectionString;
        }

        private static async void SendDeviceToMongoDbMessagesAsync(string connectionString)
        {
            double temperature = 20;
            double pressure = 900;
            double voltage = 0;
            double current = 10;

            Random rand = new Random();

            IMeasuresService measuresService = new MeasuresService(connectionString);

            while (true)
            {
                double currentTemperature = temperature + rand.NextDouble() * 15;
                double currentPressure = pressure + rand.NextDouble() * 100;
                double currentVoltage = voltage + rand.NextDouble() * 10;
                double currentCurrent = current + rand.NextDouble() * 5;

                var telemetryDataPoint = new
                {
                    deviceId = "dev-002",
                    temperature = currentTemperature,
                    pressure = currentPressure,
                    voltage = currentVoltage,
                    current = currentCurrent
                };

                var json = JsonConvert.SerializeObject(telemetryDataPoint);

                Console.WriteLine($"Inserting document: {json}");
                await measuresService.Add(json);

                Console.WriteLine("Added.");

                await Task.Delay(TimeSpan.FromSeconds(1));

            }

            // string json = "{'deviceId':'dev-001', 'temperature':25.72, 'pressure':1025.04, 'voltage':4.62, 'current':15.67}";

        }

        private static async Task GetDocumentsTest(string connectionString)
        {
            IMeasuresService measuresService = new MeasuresService(connectionString);

            var documents = await measuresService.Get();

            foreach (var item in documents)
            {
                Console.WriteLine(item);
            }

        }

        private static async Task AddDocumentTest(string connectionString)
        {

            IMeasuresService measuresService = new MeasuresService(connectionString);

            for (int i = 0; i < 100; i++)
            {
                string json = "{'deviceId':'dev-001', 'temperature':25.72, 'pressure':1025.04, 'voltage':4.62, 'current':15.67}";

                Console.WriteLine($"Inserting document: {json}");
                await measuresService.Add(json);

                Console.WriteLine("Added.");
            }
            

        }
    }
}