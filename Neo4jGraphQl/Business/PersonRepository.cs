using Neo4jGraphQl.Provider;
using System;

namespace Neo4jGraphQl.Business
{
    public interface IPersonRepository
    {
        Task<List<Dictionary<string, object>>> SearchCarRelation(string name1, string name2);
    }
    public class PersonRepository : IPersonRepository
    {
        private INeo4jDataAccess _neo4jDataAccess;

        private ILogger<PersonRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        public PersonRepository(INeo4jDataAccess neo4jDataAccess, ILogger<PersonRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        public async Task<List<Dictionary<string, object>>> SearchCarRelation(string name1, string name2)
        {
            var query = @"MATCH (c:Car)<-[rel:OWN]-(p1:Person{name:$name1})-[rel2:HAS_FRIEND|LOVES]->(p2:Person{name:$name2}) return c{name:c.name}";
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "name1", name1 },{ "name2", name2 } };
            var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "c", parameters);

            return persons;
        }
    }
}
