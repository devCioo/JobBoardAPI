using AutoMapper;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Miscellaneous;
using JobBoardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobBoardAPI.Services
{
    public interface IJobAdvertisementService
    {
        IEnumerable<JobAdvertisementDto> GetAll();
        JobAdvertisementDto GetById(int id);
        int Create(CreateJobAdvertisementDto dto);
        void Delete(int id);
        void Update(int id, UpdateJobAdvertisementDto dto);
    }
    public class JobAdvertisementService : IJobAdvertisementService
    {
        private readonly JobBoardDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<JobAdvertisementService> _logger;

        public JobAdvertisementService(JobBoardDbContext dbContext, IMapper mapper, ILogger<JobAdvertisementService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<JobAdvertisementDto> GetAll()
        {
            var jobAdvertisements = _dbContext
                .JobAdvertisements
                .Include(ja => ja.Category)
                .Include(ja => ja.Address)
                .ToList();

            var jobAdvertisementsDtos = _mapper.Map<List<JobAdvertisementDto>>(jobAdvertisements);
            return jobAdvertisementsDtos;
        }

        public JobAdvertisementDto GetById(int id)
        {
            var jobAdvertisement = _dbContext
                .JobAdvertisements
                .Include(a => a.Category)
                .Include(a => a.Address)
                .FirstOrDefault(a => a.Id == id);

            if (jobAdvertisement is null)
            {
                throw new NotFoundException("Job advertisement not found");
            }

            var jobAdvertisementDto = _mapper.Map<JobAdvertisementDto>(jobAdvertisement);
            return jobAdvertisementDto;
        }

        public int Create(CreateJobAdvertisementDto dto)
        {
            var jobAdvertisement = _mapper.Map<JobAdvertisement>(dto);
            jobAdvertisement.PostedOn = DateTime.Now;

            _dbContext.JobAdvertisements.Add(jobAdvertisement);
            _dbContext.SaveChanges();

            return jobAdvertisement.Id;
        }

        public void Delete(int id)
        {
            _logger.LogError($"Job advertisement with id: {id} DELETE action invoked");

            var jobAdvertisement = _dbContext
                .JobAdvertisements
                .FirstOrDefault(a => a.Id == id);

            if (jobAdvertisement is null)
            {
                throw new NotFoundException("Job advertisement not found");
            }

            _dbContext.JobAdvertisements.Remove(jobAdvertisement);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateJobAdvertisementDto dto)
        {
            var advertisement = _dbContext
                .JobAdvertisements
                .Include(a => a.Category)
                .Include(a => a.Address)
                .FirstOrDefault(a => a.Id == id);

            if (advertisement is null)
            {
                throw new NotFoundException("Job advertisement not found");
            }

            advertisement.Name = dto.Name;
            advertisement.CompanyName = dto.CompanyName;
            advertisement.CategoryId = dto.CategoryId;
            advertisement.Address.City = dto.City;
            advertisement.Address.Street = dto.Street;
            advertisement.Address.PostalCode = dto.PostalCode;
            advertisement.Responsibilities = dto.Responsibilities;
            advertisement.Requirements = dto.Requirements;

            _dbContext.SaveChanges();
        }
    }
}
