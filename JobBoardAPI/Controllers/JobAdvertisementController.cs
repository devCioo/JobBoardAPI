using JobBoardAPI.Entities;
using JobBoardAPI.Models;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobBoardAPI.Controllers
{
    [Route("api/jobadvertisement")]
    [ApiController]
    [Authorize(Roles = "Administrator,Employer")]
    public class JobAdvertisementController : ControllerBase
    {
        private readonly IJobAdvertisementService _jobAdvertisementService;

        public JobAdvertisementController(IJobAdvertisementService jobAdvertisementService)
        {
            _jobAdvertisementService = jobAdvertisementService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<JobAdvertisementDto>> GetAll()
        {
            var jobAdvertisementsDtos = _jobAdvertisementService.GetAll();

            return Ok(jobAdvertisementsDtos);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<JobAdvertisementDto> Get([FromRoute] int id)
        {
            var jobAdvertisementDto = _jobAdvertisementService.GetById(id);

            return Ok(jobAdvertisementDto);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateJobAdvertisementDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _jobAdvertisementService.Create(userId, dto);

            return Created($"/api/jobadvertisement/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _jobAdvertisementService.Delete(id, User);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateJobAdvertisementDto dto)
        {
            _jobAdvertisementService.Update(id, dto, User);

            return Ok();
        }
    }
}
