using Microsoft.AspNetCore.Mvc;
using Neo4jGraphQl.Business;

namespace Neo4jGraphQl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonRepository _personRepository;

        public PersonController(ILogger<PersonController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository;
        }

        [HttpGet("SearchCarRelation")]
        public async Task<object> Get(string name1= "Mike", string name2 = "Bob")
        {
            var result = await _personRepository.SearchCarRelation(name1, name2);
            return result;
        }
    }
}