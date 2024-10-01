using CompanyPortal.BLL.Services;
using CompanyPortal.BLL.Services.IServices;
using CompanyPortal.BLL.Utilities.AutoMapperProfiles;
using CompanyPortal.BLL.Utilities.Swagger;
using CompanyPortal.BLL.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CompanyPortal.BLL;

public static class DependencyInjection
{
    public static void RegisterBLLDependencies(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(typeof(AutoMapperProfiles));
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();        
        services.AddScoped<ICompanyService, CompanyService>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<UserToLoginDTOValidator>();
        services.AddValidatorsFromAssemblyContaining<UserToRegisterDTOValidator>();

        #region Versioning

        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            opt.AssumeDefaultVersionWhenUnspecified = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        #endregion

        #region Swagger

        services.AddSwaggerGen(options =>
        {
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Scheme = "bearer",
                BearerFormat = "JWT",
                Name = "JWT Authentication",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Description = "Paste your JWT Bearer token on textbox below!",

                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { jwtSecurityScheme, Array.Empty<string>() }
            });
        });

        services.ConfigureOptions<SwaggerConfigurationOptions>();

        #endregion
    }
}