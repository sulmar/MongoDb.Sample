using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb.Sample
{
    public interface IMeasuresService
    {
        Task Add(string document);

        Task<IEnumerable<dynamic>> Get();


    }
}
