﻿using Microsoft.Extensions.Options;
using Sandbox.Api.Startup.SwaggerOptions;
using Sandbox.Api.Web.Errors.Validation;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sandbox.Api.Startup;

public static class ConfigureSwagger
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        
        // Swagger examples: https://github.com/mattfrear/Swashbuckle.AspNetCore.Filters#installation
        builder.Services.AddSwaggerExamples();
        builder.Services.AddSwaggerExamplesFromAssemblyOf<ValidationErrorModelExampleProvider>();
        builder.Services.AddSwaggerGen(o =>
        {
            o.OperationFilter<ApiVersionOperationFilter>();
            o.ExampleFilters();

            var xmlPath = Path.Combine(AppContext.BaseDirectory, "Sandbox.Api.xml");
            o.IncludeXmlComments(xmlPath);
        });
        
        builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerGenOptions>();
        return builder;
    }

    public static WebApplication UseConfiguredSwagger(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) 
            return app;
        
        app.UseSwagger();
        app.UseSwaggerUI(o =>
        {
            foreach (var description in app.DescribeApiVersions())
            {
                var url = $"/swagger/{description.GroupName}/swagger.json";
                o.SwaggerEndpoint(url, description.GroupName);
            }
        });

        return app;
    }
}