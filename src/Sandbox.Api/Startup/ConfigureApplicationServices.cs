﻿using Sandbox.Api.Core;

namespace Sandbox.Api.Startup;

public static class ConfigureApplicationServices
{
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigureServices();

        return builder;
    }
}