using CompanyPortal.API.Controllers;
using CompanyPortal.BLL.Services.IServices;
using CompanyPortal.DAL.DataContext;
using CompanyPortal.DAL.Entities;
using CompanyPortal.Test.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CompanyPortal.DTO.DTOs.User;

namespace CompanyPortal.Test.IntegrationTests.API
{
    public class CompanyControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        private const string baseURL = "https://localhost:44338/";

        public CompanyControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetAllCompanies_WhenAuthTokenIsProvided_ReturnsOk()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CompanyPortalDbContext>();
                db.Database.EnsureCreated();

                // Seed the database with test data
                if (!db.Companies.Any())
                {
                    db.Companies.Add(new Company { Isin = "US1234567890", Name = "Test Company 1", Ticker = "TC1", Exchange = "NYSE", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
                    db.Companies.Add(new Company { Isin = "US0987654321", Name = "Test Company 2", Ticker = "TC2", Exchange = "NASDAQ", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
                    await db.SaveChangesAsync();
                }
            }

            // Act - Get the Bearer Token
            var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1/auth/login")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Username = "admin",
                    Password = "123"
                }), Encoding.UTF8, "application/json")
            };

            var loginResponse = await _client.SendAsync(loginRequest);
            loginResponse.EnsureSuccessStatusCode();

            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<UserToReturnDTO>(loginContent);
            var token = loginResult.Token;

            // Act - Use the Bearer Token to call GetAllCompanies
            var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/company");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.SendAsync(request);
            var companies = JArray.Parse(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(companies);
        }


        [Fact]
        public async Task CreateCompany_ExistingIsin_ReturnsBadRequest()
        {
            // Arrange
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CompanyPortalDbContext>();
                db.Database.EnsureCreated();

                // Seed the database with a company having the same ISIN
                if (!db.Companies.Any(c => c.Isin == "US1234567890"))
                {
                    db.Companies.Add(new Company { Isin = "US1234567890", Name = "Existing Company", Ticker = "EXC", Exchange = "NYSE", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
                    await db.SaveChangesAsync();
                }
            }

            // Act - Get the Bearer Token
            var loginRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1/auth/login")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Username = "admin",
                    Password = "123"
                }), Encoding.UTF8, "application/json")
            };

            var loginResponse = await _client.SendAsync(loginRequest);
            loginResponse.EnsureSuccessStatusCode();

            var loginContent = await loginResponse.Content.ReadAsStringAsync();
            var loginResult = JsonConvert.DeserializeObject<UserToReturnDTO>(loginContent);
            var token = loginResult.Token;

            // Act - Try to create a new company with the same ISIN
            var createCompanyRequest = new HttpRequestMessage(HttpMethod.Post, "api/v1/company")
            {
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    Isin = "US1234567890",
                    Name = "New Company",
                    StockTicker = "NEW",
                    Exchange = "NASDAQ"
                }), Encoding.UTF8, "application/json")
            };
            createCompanyRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var createCompanyResponse = await _client.SendAsync(createCompanyRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, createCompanyResponse.StatusCode);
        }


    }
}
