using AutoMapper;
using CompanyPortal.BLL.Services;
using CompanyPortal.BLL.Services.IServices;
using CompanyPortal.BLL.Utilities.AutoMapperProfiles;
using CompanyPortal.BLL.Utilities.CustomExceptions;
using CompanyPortal.DAL.Entities;
using CompanyPortal.DAL.Repositories.IRepositories;
using CompanyPortal.DTO.DTOs.Company;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;

namespace CompanyPortal.Test.UnitTests.BLL.Services
{
    public class CompanyServiceTests
    {
        private readonly ICompanyService _companyService;
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly Mock<ILogger<CompanyService>> _logger;
        private readonly IMapper _mapper;

        private readonly Company _companyEntity;
        private readonly CompanyDTO _companyDTO;

        public CompanyServiceTests()
        {
            _companyEntity = new Company
            {
                Id = 1,
                Isin = "US1234567890",
                Name = "Test Company",
                Ticker = "TC",
                Exchange = "NYSE",
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            _companyDTO = new CompanyDTO
            {
                Id = 1,
                Isin = "US1234567890",
                Name = "Test Company",
                Ticker = "TC",
                Exchange = "NYSE"
            };

            _companyRepository = new Mock<ICompanyRepository>();

            _logger = new Mock<ILogger<CompanyService>>();

            var myProfile = new AutoMapperProfiles.AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);

            _companyService = new CompanyService(_companyRepository.Object, _mapper, _logger.Object);
        }

        [Fact]
        public async Task CreateCompany_ValidCompany_ReturnsCompanyDTO()
        {
            // Arrange
            _companyRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Company, bool>>>(), default))
                .ReturnsAsync((Company)null!);

            _companyRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Company>()))
                .ReturnsAsync(_companyEntity);

            // Act
            var result = await _companyService.CreateCompanyAsync(_companyDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<CompanyDTO>(result);
            Assert.Equal(_companyEntity.Id, result.Id);
        }

        [Fact]
        public async Task CreateCompany_DuplicateIsin_ThrowsDuplicateIsinException()
        {
            // Arrange
            _companyRepository
                .Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Company, bool>>>(), default))
                .ReturnsAsync(_companyEntity);

            // Act & Assert
            await Assert.ThrowsAsync<DuplicateIsinException>(() => _companyService.CreateCompanyAsync(_companyDTO));
        }
    }
}
