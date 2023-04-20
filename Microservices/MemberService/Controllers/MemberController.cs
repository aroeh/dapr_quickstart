using MemberService.Models;
using MemberService.Repo;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IMemberRepo memberRepo;

        public MemberController(ILogger<MemberController> logger, IMemberRepo dojo)
        {
            _logger = logger;
            memberRepo = dojo;
        }

        /// <summary>
        /// Gets all available members from the data store
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var members = await memberRepo.GetAll();

            if(members == null || !members.Any())
            {
                return NoContent();
            }

            return Ok(members);
        }

        /// <summary>
        /// Gets the specified member using the supplied id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var member = await memberRepo.Get(id);

            if(member == null || member.Id == string.Empty)
            {
                return NoContent();
            }

            return Ok(member);
        }

        /// <summary>
        /// Inserts a new member to the data store
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MemberRecord member)
        {
            var result = await memberRepo.Create(member);

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing member record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MemberRecord member)
        {
            var result = await memberRepo.Update(member);

            return Ok(result);
        }
    }
}