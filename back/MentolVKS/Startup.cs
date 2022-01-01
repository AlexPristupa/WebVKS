using AspNetCore.Proxy;
using MentolVKS.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MentolVKS.LdapAuth;
using Microsoft.AspNetCore.Identity;using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Microsoft.Extensions.Logging.Debug;
using Serilog;
using System;
using System.IO;
using MentolVKS.Tools;
using MentolVKS.Data.EF.Settings;
using MentolVKS.Service.Contract;
using MentolVKS.Data.Interfaces;
using MentolVKS.Data;
using MentolVKS.Data.EF.DependencyInjection;
using MentolVKS.Model.BaseModel;
using NSwag.Generation.Processors.Security;
using System.Linq;
using NSwag;
using MentolVKS.Model;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using MentolVKS.Auth.Jwt;

namespace MentolVKS
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конфиг
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Имя настроек CORS
        /// </summary>
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            #region Swagger
            services.AddSwaggerDocument(config =>
            {
                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT token"));

                config.AddSecurity("JWT token", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "Copy 'Bearer ' + valid JWT token into field",
                    In = OpenApiSecurityApiKeyLocation.Header
                });
                config.PostProcess = (document) =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "MyRest-API";
                    document.Info.Description = "ASP.NET Core 3.1 MyRest-API";
                };
            });
            #endregion
 
            services.Configure<IISOptions>(options =>
            {
                options.AuthenticationDisplayName = "Windows";
                options.AutomaticAuthentication = false;
            });

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en"),
                    new CultureInfo("ru")
                };

                opts.DefaultRequestCulture = new RequestCulture("en-US");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
            });

            var dbConfiguration = Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
           
            services.Configure<List<LdapAuth.Model.LdapConfig>>(Configuration.GetSection(nameof(LdapAuth.Model.LdapConfig)));
            services.Configure<DatabaseSettings>(Configuration.GetSection(nameof(DatabaseSettings)));
            services.Configure<SessionSettings>(Configuration.GetSection(nameof(SessionSettings)));
            services.AddLdapAuthService();

            services.AddProxies();
            services.AddHttpClient();

            services.AddHttpContextAccessor();
            services.AddTransient<IUserInterface,UserName>();

            services.AddEFRepositories(dbConfiguration);
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IService, Service.Service>();

            /*services.AddScoped<IPasswordHasher<AspNetUser>, PasswordHasher>();
            services.AddTransient<IUserStore<AspNetUser>, UserStore>();
            services.AddTransient<IRoleStore<Model.BaseModel.AspNetRole>, RoleStore>();
            services.AddIdentity<AspNetUser,AspNetRole>()
                .AddUserStore<UserStore>()
                .AddRoleStore<RoleStore>()
                .AddDefaultTokenProviders();*/


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.IsUser,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("*");
                                      builder.AllowAnyMethod();
                                      builder.AllowAnyHeader();
                                  });
            });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddControllersWithViews()
                .ConfigureApiBehaviorOptions(options => {
                    options.SuppressModelStateInvalidFilter = true;
                })
                .AddDataAnnotationsLocalization()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateFormatString = "dd.MM.yyyy HH:mm:ss";
                    options.UseCamelCasing(true);
                });
            services.AddSpaStaticFiles(configuration => configuration.RootPath = "wwwroot");
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
           
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("ru"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            /*Proxy для оборудования Cisco*/
            app.Use(next => async context =>
            {
                if (context.Request.Path.StartsWithSegments("/proxy"))
                {
                    var request = context.Request;
                     HttpClient _client = new HttpClient();
                    Uri uri = new Uri(Configuration.GetSection("Service")["EndPoint"] + request.Path.Value.Replace("/proxy", ""));
                    _client.BaseAddress = uri;
                    _client.Timeout = TimeSpan.FromMinutes(5);

                    var requestMessage = new HttpRequestMessage();
                    var requestMethod = request.Method;

                    var streamContent = new StreamContent(request.Body);
                    requestMessage.Content = streamContent;


                    // Copy the request headers
                    foreach (var header in request.Headers)
                    {
                        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                        {
                            requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                        }
                    }

                    requestMessage.Headers.Host = uri.Authority;
                    requestMessage.RequestUri = uri;
                    requestMessage.Method = new HttpMethod(request.Method);

                    var responseMessage = await _client.SendAsync(requestMessage);

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Methods", "*");
                    context.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "application/json";

                    if (!responseMessage.IsSuccessStatusCode)
                    {
                        context.Response.WriteAsync(JsonConvert.SerializeObject(new { result = -1, data = "" }));
                        return;
                    }

                    var result = await responseMessage.Content.ReadAsStringAsync();
                    try
                    {
                        var value = JsonConvert.DeserializeObject(result);
                        context.Response.WriteAsync(JsonConvert.SerializeObject(new { result = 0, data = value }));
                    }
                    catch {
                        context.Response.WriteAsync(JsonConvert.SerializeObject(new { result = 0, data = result }));
                    }

                    
                    return;
                }

                await next(context);
            });
            
            //app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();            
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";                
            });
            
        }
    }
}
