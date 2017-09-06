﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace PubgStatistics {
  public class Startup {
    /// <summary>
    /// Startup
    /// </summary>
    /// <param name="env"></param>
    public Startup(IHostingEnvironment env) {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }
    /// <summary>
    /// Configuration
    /// </summary>
    public IConfigurationRoot Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services) {
      // Add framework services.
      services.AddMvc();
      //configure CORS policy to allow all
      services.AddCors(options => {
        options.AddPolicy("AllowAll",
            builder => builder.WithOrigins("*").WithMethods("*").WithHeaders("*"));
      });
      //configure swagger generation
      services.AddSwaggerGen(config => {
        config.SwaggerDoc("v1", new Info { Title = "PU Battlegrounds Statistics", Version = "v1" });
        config.IncludeXmlComments("PubgStatistics.Api.xml");
      });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="loggerFactory"></param>
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
      loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      loggerFactory.AddDebug();

      app.UseMvc();
      //apply CORS policy
      app.UseCors("AllowAll");
      // Enable the swagger UI
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
      });
      //start swagger
      app.UseSwagger();
    }
  }
}
