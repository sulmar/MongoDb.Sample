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

        public MyContext(string connectionString)
        {
            var client = new MongoClient(connectionString);

            var databaseName = MongoUrl.Create(connectionString).DatabaseName;

            if (client != null)
                _database = client.GetDatabase(databaseName);
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
