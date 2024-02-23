using JobBoardAPI.Entities;
using JobBoardAPI.Models;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/advertisement")]
    [ApiController]
    public class JobAdvertisementController : ControllerBase
    {
        private readonly IJobAdvertisementService _jobAdvertisementService;

        public JobAdvertisementController(IJobAdvertisementService jobAdvertisementService)
        {
            _jobAdvertisementService = jobAdvertisementService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<JobAdvertisement>> GetAll()
        {
            var jobAdvertisementsDtos = _jobAdvertisementService.GetAll();

            return Ok(jobAdvertisementsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<JobAdvertisement> Get([FromRoute] int id)
        {
            var jobAdvertisement = _jobAdvertisementService.GetById(id);

            return Ok(jobAdvertisement);
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateJobAdvertisementDto dto)
        {
            var id = _jobAdvertisementService.Create(dto);

            return Created($"/api/advertisement/{id}", null);
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
