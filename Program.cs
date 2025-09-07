using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using NZWalks.API.Data;
using NZWalks.API.Data.Interceptors;
using NZWalks.API.IRepositories;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using NZWalks.API.Repositorys.IRepositories;
using NZWalks.API.Repositorys.Repositories;
using System.Text;

// ##  //7.0.3
namespace NZWalks.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // 添加 CORS 服务
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500") // 前端地址
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials(); // 如果你用 JWT 不需要这句
                });
            });



            // 添加服务
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {

                //设置JSON返回日期格式
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                //统一设置API的日期格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            //注入数据库上下文
            builder.Services.AddDbContext<NZWalksDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("NzWalksConnectionString"));
                options.AddInterceptors(new AuditableInterceptor());
            });

            //?
            builder.Services.AddDbContext<NZWalksAuthDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"))

            );
       
            builder.Services.AddScoped<IRegionRepository, RegionRepository>();
            builder.Services.AddScoped<IWalkRepository, WalkRepository>();
            builder.Services.AddScoped<ITokenPepository, TokenPepository>();

            builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



            builder.Services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
                .AddEntityFrameworkStores<NZWalksAuthDbContext>()
                .AddDefaultTokenProviders();



            builder.Services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequireLowercase = false;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequiredLength = 6;
                Options.Password.RequiredUniqueChars = 1;
            });





            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                });



            var app = builder.Build();



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowFrontend");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
