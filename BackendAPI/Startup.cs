using BackendAPI.Business;
using BackendAPI.Business.Abstract;
using BackendAPI.Business.Concrete;
using BackendAPI.DataAccess;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.DataAccess.Concrete;
using BackendAPI.Entities;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAPI
{
    public class Startup
    {


        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}


        //public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
        //    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //    services.AddScoped<ITasinmazService, TasinmazService>();
        //    services.AddScoped<IUserService, UserService>();
        //    services.AddScoped<IAuthRepository, AuthRepository>();
        //    services.AddScoped<IRepository<Tasinmaz>, Repository<Tasinmaz>>();
        //    services.AddScoped<ILogService, LogService>();
        //    services.AddScoped<IIlceRepository, IlceRepository>();

        //    services.AddScoped<IRepository<Ilce>, Repository<Ilce>>();

        //    services.AddScoped<IIlceService, IlceService>();

        //    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        //    {
        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = new SymmetricSecurityKey(key),
        //            ValidateIssuer = false,
        //            ValidateAudience = false
        //        };
        //    });
        //    // Controller desteğini ekler
        //    services.AddControllers();
        //    services.AddControllersWithViews()
        //        .AddNewtonsoftJson(options =>
        //        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //    );
        //    services.AddCors(options =>
        //    {
        //        options.AddPolicy("AllowAllOrigins", builder =>
        //        {
        //            builder.AllowAnyOrigin() // Tüm origin'lere izin ver
        //                   .AllowAnyHeader()  // Tüm header'lara izin ver
        //                   .AllowAnyMethod(); // GET, POST, PUT, DELETE vb. tüm HTTP metotlarına izin ver
        //        });
        //    });

        //    // Swagger/OpenAPI desteğini ekler
        //    services.AddSwaggerGen(c =>
        //    {
        //        c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        //        {
        //            Title = "Tasinmaz API",
        //            Version = "v1",
        //            Description = "Tasinmaz Yönetim API'si"
        //        });
        //        c.UseInlineDefinitionsForEnums();
        //    });

        //    // PostgreSQL bağlantısı
        //    var connectionString = Configuration.GetConnectionString("DefaultConnection");
        //    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

        //    #region Dependency Injection
        //    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        //    services.AddScoped<ITasinmazService, TasinmazService>();
        //    services.AddScoped<IIlService, IlService>();
        //    services.AddScoped<IIlceService, IlceService>();
        //    services.AddScoped<IMahalleService, MahalleService>();
        //    #endregion

        //}


        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        //{
        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseRouting();
        //    app.UseAuthorization();
        //    app.UseCors("AllowAllOrigins");
        //    app.UseAuthentication();

        //    // Swagger
        //    app.UseSwagger();
        //    app.UseSwaggerUI(c =>
        //    {
        //        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasinmaz API V1");
        //        c.RoutePrefix = string.Empty;
        //    });

        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapControllers();
        //    });
        //}


















      
        
            public Startup(IConfiguration configuration)
            {
                Configuration = configuration;
            }

            public IConfiguration Configuration { get; }

            // This method gets called by the runtime. Use this method to add services to the container.
            public void ConfigureServices(IServiceCollection services)
            {
                var key = Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value);
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped<ITasinmazService, TasinmazService>();
                services.AddScoped<IUserService, UserService>();
                services.AddScoped<IAuthRepository, AuthRepository>();
                services.AddScoped<IRepository<Tasinmaz>, Repository<Tasinmaz>>();
                services.AddScoped<ILogService, LogService>();
                services.AddScoped<IIlceRepository, IlceRepository>();
                services.AddScoped<IRepository<Ilce>, Repository<Ilce>>();
                services.AddScoped<IIlceService, IlceService>();

                // 🔹 JWT Authentication Ayarları
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

                // 🔹 Controller desteğini ekler
                services.AddControllers();
                services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

                // 🔹 CORS Politikası (Angular 4200 portuna izin verildi)
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowAllOrigins", builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // 🔹 Angular'ın çalıştığı URL'yi kullan
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials(); // 🔹 JWT Token ile çalışıyorsan eklemelisin
                    });
                });

                // 🔹 Swagger Konfigürasyonu (JWT Authentication Desteği Eklendi!)
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Tasinmaz API",
                        Version = "v1",
                        Description = "Tasinmaz Yönetim API'si"
                    });

                    // 🔹 Swagger için JWT Yetkilendirme Desteği
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Token girerken 'Bearer <TOKEN>' formatında girin"
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
                    new string[] {}
                }
            });
                });

                // PostgreSQL bağlantısı
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

                #region Dependency Injection
                services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
                services.AddScoped<ITasinmazService, TasinmazService>();
                services.AddScoped<IIlService, IlService>();
                services.AddScoped<IIlceService, IlceService>();
                services.AddScoped<IMahalleService, MahalleService>();
                #endregion
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseHttpsRedirection();

                app.UseRouting();

                // 🔹 CORS Middleware buraya eklendi!
                app.UseCors("AllowAllOrigins");

                app.UseAuthentication(); // 🔹 JWT Token için Authentication Middleware
                app.UseAuthorization(); // 🔹 Yetkilendirme Middleware

                // 🔹 Swagger Middleware
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tasinmaz API V1");
                    c.RoutePrefix = string.Empty;
                });

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            }
        





    }
}
