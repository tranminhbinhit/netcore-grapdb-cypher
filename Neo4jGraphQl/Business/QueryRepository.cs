using Neo4jGraphQl.Models.Query;
using Neo4jGraphQl.Provider;
using System;
using System.Reflection.Metadata.Ecma335;

namespace Neo4jGraphQl.Business
{
    public interface IQueryRepository
    {
        Task<List<Dictionary<string, object>>> QueryCypher(QueryCypherReq req);
    }
    public class QueryRepository : IQueryRepository
    {
        private INeo4jDataAccess _neo4jDataAccess;

        private ILogger<QueryRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryRepository"/> class.
        /// </summary>
        public QueryRepository(INeo4jDataAccess neo4jDataAccess, ILogger<QueryRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        public async Task<List<Dictionary<string, object>>> QueryCypher(QueryCypherReq req)
        {
            //IDictionary<string, object> parameters = new Dictionary<string, object> { };
            var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(req.Query,req.ReturnObject, null);
            return persons;
        }
    }
}
