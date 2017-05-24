using MongoDb.Sample.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDb.Sample
{
    public class MyContext
    {
        private readonly IMongoDatabase _database = null;

        public MyContext(Settings settings)
        {
            var client = new MongoClient(settings.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Database);
        }


        public IMongoCollection<BsonDocument> Measures
        {
            get
            {
                return _database.GetCollection<BsonDocument>("Measures");
            }
        }
    }
}
