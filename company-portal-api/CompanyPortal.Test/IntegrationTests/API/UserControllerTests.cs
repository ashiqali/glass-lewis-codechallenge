using CompanyPortal.DAL.DataContext;
using CompanyPortal.DAL.Entities;
using CompanyPortal.DTO.DTOs.User;
using CompanyPortal.Test.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace CompanyPortal.Test.IntegrationTests.API;

public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    private const string baseURL = "https://localhost:44338/";

    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task GetUsers_WhenAuthTokenIsNotProvided_ReturnsSuccess()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CompanyPortalDbContext>();
            db.Database.EnsureCreated();

            // Seed the database with test data
            if (!db.Users.Any())
            {
                db.Users.Add(new User { Username = "testuser", Password = HashPassword("123"), Name = "Test", Surname = "User", CreatedDate = DateTime.UtcNow, ModifiedDate = DateTime.UtcNow });
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

        // Act - Use the Bearer Token to call GetUsers
        var request = new HttpRequestMessage(HttpMethod.Get, "api/v1/user");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.SendAsync(request);
        var users = JArray.Parse(await response.Content.ReadAsStringAsync());

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task GetUsers_WhenAuthTokenIsNotProvided_ReturnsUnauthorized()
    {
        // Arrange
        using (var scope = _factory.Services.CreateScope())
        {
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<CompanyPortalDbContext>();
            db.Database.EnsureCreated();
        }

        // Act
        var response = await _client.GetAsync("api/v1/user");

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private string HashPassword(string password)
    {
        var passwordHasher = new PasswordHasher<object>();
        return passwordHasher.HashPassword(null, password);
    }
}
