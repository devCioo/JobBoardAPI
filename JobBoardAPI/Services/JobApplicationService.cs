using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Miscellaneous;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public interface IJobApplicationService
    {
        List<JobApplicationDto> GetAll(int jobAdvertisementId);
        JobApplicationDto GetById(int jobAdvertisementId, int jobApplicationId);
        int Create(int jobAdvertisementId, CreateJobApplicationDto dto);
        void Delete(int jobAdvertisementId, int jobApplicationId);
    }

    public class JobApplicationService : IJobApplicationService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly IMapper _mapper;

        public JobApplicationService(JobBoardDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public JobApplicationDto GetById(int jobAdvertisementId, int jobApplicationId)
        {
            var jobAdvertisement = GetJobAdvertisementById(jobAdvertisementId);

            var jobApplication = _dbContext
                .JobApplications
                .FirstOrDefault(ja => ja.Id == jobApplicationId);

            if (jobApplication is null || jobApplication.JobAdvertisementId != jobAdvertisementId)
            {
                throw new NotFoundException("Job application not found");
            }

            var jobApplicationDto = _mapper.Map<JobApplicationDto>(jobApplication);

            return jobApplicationDto;
        }

        public List<JobApplicationDto> GetAll(int jobAdvertisementId)
        {
            var jobAdvertisement = GetJobAdvertisementById(jobAdvertisementId);

            var jobApplicationDtos = _mapper.Map<List<JobApplicationDto>>(jobAdvertisement.JobApplications);

            return jobApplicationDtos;
        }

        public int Create(int jobAdvertisementId, CreateJobApplicationDto dto)
        {
            var jobAdvertisement = GetJobAdvertisementById(jobAdvertisementId);

            var jobApplication = _mapper.Map<JobApplication>(dto);
            jobApplication.AppliedOn = DateTime.Now;
            jobApplication.JobAdvertisementId = jobAdvertisementId;

            _dbContext.JobApplications.Add(jobApplication);
            _dbContext.SaveChanges();

            return jobApplication.Id;
        }

        public void Delete(int jobAdvertisementId, int jobApplicationId)
        {
            var jobAdvertisement = GetJobAdvertisementById(jobAdvertisementId);

            var jobApplication = _dbContext
                .JobApplications
                .FirstOrDefault(ja => ja.Id == jobApplicationId);

            if (jobApplication is null || jobApplication.JobAdvertisementId != jobAdvertisementId)
            {
                throw new NotFoundException("Job application not found");
            }

            _dbContext.Remove(jobApplication);
            _dbContext.SaveChanges();
        }

        private JobAdvertisement GetJobAdvertisementById(int id)
        {
            var jobAdvertisement = _dbContext
                .JobAdvertisements
                .Include(ja => ja.JobApplications)
                .FirstOrDefault(ja => ja.Id == id);

            if (jobAdvertisement == null)
            {
                throw new NotFoundException("Job advertisement not found");
            }

            return jobAdvertisement;
        }
    }
}