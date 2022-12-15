using System;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using DataModel.Infrastructure.Database;
using DataModel.Infrastructure.Implementations;
using DataModel.Infrastructure.Interfaces;
using DataModel.Infrastructure.Interfaces.Query;
using DataModel.Infrastructure.Models;
using DataModel.Infrastructure.Query;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TranTriTaiBlog.Filter;
using TranTriTaiBlog.Infrastructures.AutoMappers;
using TranTriTaiBlog.Infrastructures.Constants;
using TranTriTaiBlog.Infrastructures.Intefaces;
using TranTriTaiBlog.Infrastructures.Intefaces.UserServices;
using TranTriTaiBlog.Infrastructures.Services;
using TranTriTaiBlog.Infrastructures.Services.UserServices;

namespace TranTriTaiBlog
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
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            services.AddRouting(o => o.LowercaseUrls = true);

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            AddDbContext(services);

            RegisterServices(services);

            AddTokenAuthentication(services);

            AddSwaggerGen(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ApplyEFMigration(app);

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "TranTriTai Blog");
            });

            app.UseExceptionHandler("/api/error");

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(o => o.AllowAnyOrigin());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ApplyEFMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using var context = scope.ServiceProvider.GetService<BlogDbContext>();
                for (int i = 0; i <= 3; i++)
                {
                    try
                    {
                        Console.WriteLine($"Try execute migrate command time: {i}");
                        context?.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error occured while trying to apply migrations: {ex.Message}");
                    }
                }
            }
        }

        private void AddDbContext(IServiceCollection services)
        {
            var connstr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<BlogDbContext>(o =>
            {
                o.UseLazyLoadingProxies();
                o.UseMySql(connstr, ServerVersion.AutoDetect(connstr));
            });
        }

        private void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
                o.EnableAnnotations();
            });
        }

        private void AddTokenAuthentication(IServiceCollection services)
        {
            var secret = Configuration.GetConnectionString("TokenSecret");
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            //DI for queries
            services.AddScoped<IUserQuery, UserQuery>();
            services.AddScoped<ISkillQuery, SkillQuery>();

            //DI for Repos
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //DI for services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IPostService, PostService>();

            //services.AddSingleton<ApiAuthentication>();
            //services.AddSingleton<HttpContent>();
        }
    }
}

