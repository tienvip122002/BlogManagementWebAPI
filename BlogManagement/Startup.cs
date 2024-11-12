using Alachisoft.NCache.Caching.Distributed;
using AutoMapper;
using BlogManagement.Domain.Configurations;
using BlogManagement.Infrastructure.Configuration;
using BlogManagement.WebAPI.Configuration;
using BlogManagement.WebAPI.Middleware;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using ProtoBuf.Extended.Meta;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			RegisterMapper(services, Configuration);

			//nlog
			services.AddLogging(logging =>
			{
				logging.AddNLog();
				logging.ClearProviders();
				logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Warning);
			});

			//global authen
			//services.AddAuthorization(options =>
			//{

			//	options.FallbackPolicy = new AuthorizationPolicyBuilder()
			//									.RequireAuthenticatedUser()
			//									.Build();

			//});

			//automapper
			//services.AddAutoMapper(typeof(AutoMapperConfig).Assembly);


			//cache
			services.AddNCacheDistributedCache(configuration =>
			{
				configuration.CacheName = "BlogManagementCache";
				configuration.EnableLogs = true;
				configuration.ExceptionsEnabled = true;
			});

			services.RegisterContextDb(Configuration);

			//Register Dependency Injection
			services.RegisterDI(Configuration);

			services.Configure<ConnectionStrings>(Configuration.GetSection(nameof(ConnectionStrings)));


			services.RegisterTokenBear(Configuration);

			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BlogManagement", Version = "v1" });
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.Http,
					In = ParameterLocation.Header,
					BearerFormat = "JWT",
					Scheme = "Bearer",
					Description = "Please input your token"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
			});
			//fluentvalidation
			services.AddValidatorsFromAssemblyContaining<Startup>();
		}

		private static void RegisterMapper(IServiceCollection services, IConfiguration configuration)
		{
			var urlServerDomain = configuration.GetSection("JwtIssuerOptions:Audience");
			var urlServer = urlServerDomain?.Value;
			var config = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperConfig());
				cfg.AddProfile(new SysFileMapping());


			});
			var mapper = config.CreateMapper();
			services.AddSingleton(mapper);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (string.IsNullOrWhiteSpace(env.WebRootPath))
			{
				env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			}
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseStaticFiles();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BlogManagement v1"));
			}

			app.UseHttpsRedirection();

			////Handle Global Error
			//app.UseExceptionHandler(error =>
			//{
			//	error.Run(async httpContext =>
			//	{
			//		var msg = httpContext.Features.Get<IExceptionHandlerFeature>();

			//		int statusCode = httpContext.Response.StatusCode;

			//		await httpContext.Response.WriteAsync($"{statusCode} - {msg.Error.Message}");

			//	});
			//});

			app.UseMiddleware<ExceptionMiddleware>();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
