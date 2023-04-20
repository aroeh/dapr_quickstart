using DojoService.Models;
using DojoService.Repo;
using Microsoft.AspNetCore.Mvc;

namespace DojoService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DojoController : ControllerBase
    {
        private readonly ILogger<DojoController> _logger;
        private readonly IDojoRepo dojoRepo;

        public DojoController(ILogger<DojoController> logger, IDojoRepo dojo)
        {
            _logger = logger;
            dojoRepo = dojo;
        }

        /// <summary>
        /// Gets all available dojos from the data store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var dojos = await dojoRepo.GetAll();

            if(dojos == null || !dojos.Any())
            {
                return NoContent();
            }

            return Ok(dojos);
        }

        /// <summary>
        /// Gets the specified dojo using the supplied id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var dojo = await dojoRepo.Get(id);

            if(dojo == null || dojo.Id == string.Empty)
            {
                return NoContent();
            }

            return Ok(dojo);
        }

        /// <summary>
        /// Inserts a new Dojo to the data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dojo dojo)
        {
            var result = await dojoRepo.Create(dojo);

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing dojo record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Dojo dojo)
        {
            var result = await dojoRepo.Update(dojo);

            return Ok(result);
        }
    }
}