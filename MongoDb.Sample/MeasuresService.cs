using MongoDb.Sample.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.Sample
{
    public class MeasuresService : IMeasuresService
    {
        private readonly MyContext context = null;

        public MeasuresService(Settings settings)
        {
            context = new MyContext(settings);
        }

        public async Task Add(string json)
        {
            var document = BsonDocument.Parse(json);

            await context.Measures.InsertOneAsync(document);
        }

        public async Task<IEnumerable<dynamic>> Get()
        {
            var documents = await context.Measures.FindAsync(FilterDefinition<BsonDocument>.Empty);

            return await documents.ToListAsync();

        }
    }
}
