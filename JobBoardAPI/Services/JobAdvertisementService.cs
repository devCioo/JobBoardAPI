using AutoMapper;
using JobBoardAPI.Authorization;
using JobBoardAPI.Entities;
using JobBoardAPI.Exceptions;
using JobBoardAPI.Miscellaneous;
using JobBoardAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public JobAdvertisementService(JobBoardDbContext dbContext, IMapper mapper, ILogger<JobAdvertisementService> logger, 
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
            jobAdvertisement.UserId = _userContextService.GetUserId;

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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, jobAdvertisement,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _dbContext.JobAdvertisements.Remove(jobAdvertisement);
            _dbContext.SaveChanges();
        }

        public void Update(int id, UpdateJobAdvertisementDto dto)
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

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, jobAdvertisement, 
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            jobAdvertisement.Name = dto.Name;
            jobAdvertisement.CompanyName = dto.CompanyName;
            jobAdvertisement.CategoryId = dto.CategoryId;
            jobAdvertisement.Address.City = dto.City;
            jobAdvertisement.Address.Street = dto.Street;
            jobAdvertisement.Address.PostalCode = dto.PostalCode;
            jobAdvertisement.Responsibilities = dto.Responsibilities;
            jobAdvertisement.Requirements = dto.Requirements;

            _dbContext.SaveChanges();
        }
    }
}
