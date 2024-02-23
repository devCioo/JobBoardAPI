﻿using JobBoardAPI.Models;
using JobBoardAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobBoardAPI.Controllers
{
    [Route("api/jobadvertisement/{jobAdvertisementId}/jobapplication")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpGet]
        public ActionResult<List<JobApplicationDto>> GetAll([FromRoute] int jobAdvertisementId)
        {
            var jobApplicationDtos = _jobApplicationService.GetAll(jobAdvertisementId);

            return Ok(jobApplicationDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<JobApplicationDto> Get([FromRoute] int jobAdvertisementId, [FromRoute] int id)
        {
            var jobApplicationDto = _jobApplicationService.GetById(jobAdvertisementId, id);

            return Ok(jobApplicationDto);
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int jobAdvertisementId, [FromBody] CreateJobApplicationDto dto)
        {
            var id = _jobApplicationService.Create(jobAdvertisementId, dto);

            return Created($"/api/jobadvertisement/{jobAdvertisementId}/jobapplication/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int jobAdvertisementId, [FromRoute] int id)
        {
            _jobApplicationService.Delete(jobAdvertisementId, id);

            return NoContent();
        }
    }
}
