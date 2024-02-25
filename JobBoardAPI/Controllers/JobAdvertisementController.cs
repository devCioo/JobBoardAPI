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
        public ActionResult<IEnumerable<JobAdvertisementDto>> GetAll([FromQuery] JobAdvertisementQuery query)
        {
            var jobAdvertisementsDtos = _jobAdvertisementService.GetAll(query);

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
            var id = _jobAdvertisementService.Create(dto);

            return Created($"/api/jobadvertisement/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _jobAdvertisementService.Delete(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateJobAdvertisementDto dto)
        {
            _jobAdvertisementService.Update(id, dto);

            return Ok();
        }
    }
}
