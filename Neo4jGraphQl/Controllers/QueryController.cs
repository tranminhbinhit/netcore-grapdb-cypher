using Microsoft.AspNetCore.Mvc;
using Neo4jGraphQl.Business;
using Neo4jGraphQl.Models.Query;

namespace Neo4jGraphQl.Controllers
{
    [ApiController]
    [Route("query")]
    public class QueryController : ControllerBase
    {
        private readonly ILogger<QueryController> _logger;
        private readonly IQueryRepository _queryRepository;

        public QueryController(ILogger<QueryController> logger, IQueryRepository queryRepository)
        {
            _logger = logger;
            _queryRepository = queryRepository;
        }

        [HttpPost("cypher")]
        public async Task<object> QueryCypher(QueryCypherReq req)
        {
            var result = await _queryRepository.QueryCypher(req);
            return result;
        }
    }
}