using Neo4jGraphQl.Provider;
using System;

namespace Neo4jGraphQl.Business
{
    public interface ITestRepository
    {
        //Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString);
        //Task<bool> AddPerson(Person person);
        Task<List<Dictionary<string, object>>> SearchPersonsByName(string name1, string name2);
    }
    public class TestRepository : ITestRepository
    {
        private INeo4jDataAccess _neo4jDataAccess;

        private ILogger<TestRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestRepository"/> class.
        /// </summary>
        public TestRepository(INeo4jDataAccess neo4jDataAccess, ILogger<TestRepository> logger)
        {
            _neo4jDataAccess = neo4jDataAccess;
            _logger = logger;
        }

        public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string name1, string name2)
        {
            var query = @"MATCH (c:Car)<-[rel:OWN]-(p1:Person{name:$name1})-[rel2:HAS_FRIEND|LOVES]->(p2:Person{name:$name2}) return c{name:c.name}";
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "name1", name1 },{ "name2", name2 } };
            var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "c", parameters);

            return persons;
        }

        ///// <summary>
        ///// Searches the name of the person.
        ///// </summary>
        //public async Task<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
        //{
        //    var query = @"MATCH (p:Person) WHERE toUpper(p.name) CONTAINS toUpper($searchString) 
        //                        RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

        //    IDictionary<string, object> parameters = new Dictionary<string, object> { { "searchString", searchString } };

        //    var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "p", parameters);

        //    return persons;
        //}

        ///// <summary>
        ///// Adds a new person
        ///// </summary>
        //public async Task<bool> AddPerson(Person person)
        //{
        //    if (person != null && !string.IsNullOrWhiteSpace(person.Name))
        //    {
        //        var query = @"MERGE (p:Person {name: $name}) ON CREATE SET p.born = $born 
        //                    ON MATCH SET p.born = $born, p.updatedAt = timestamp() RETURN true";
        //        IDictionary<string, object> parameters = new Dictionary<string, object>
        //        {
        //            { "name", person.Name },
        //            { "born", person.Born ?? 0 }
        //        };
        //        return await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);
        //    }
        //    else
        //    {
        //        throw new System.ArgumentNullException(nameof(person), "Person must not be null");
        //    }
        //}

        ///// <summary>
        ///// Get count of persons
        ///// </summary>
        //public async Task<long> GetPersonCount()
        //{
        //    var query = @"Match (p:Person) RETURN count(p) as personCount";
        //    var count = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);
        //    return count;
        //}
    }
}
