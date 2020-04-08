using JWT.Server;
using JWT.Server.Services;
using JWT.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Mile.JWT.Server
{
    public class Startup
    {

        private static Dictionary<string, string> _accounts;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _accounts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            _accounts.Add("Foo", "password");
            _accounts.Add("Bar", "password");
            _accounts.Add("Baz", "password");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            services.AddControllers();
            services.AddRouting();
            //JwtÑéÖ¤
            var key = Encoding.ASCII.GetBytes(Const.SecurityKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "Yang",
                    ValidIssuer = "Yang",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

            //services.AddAuthentication(option => option.DefaultAuthenticateScheme =
            //CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ITryDelegateService, TryDelegateService>();
            services.AddScoped<IAsParallelLinQService, AsParallelLinQService>();
            services.AddScoped<IAggregateLinQService, AggregateLinQService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");

            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Map(pattern: "/", RenderHomePageAsync);
                endpoints.Map("Account/Login", SignInAsync);
                endpoints.Map("Account/Logout", SignOutAsync);
            });
        }

        public async Task RenderHomePageAsync(HttpContext httpContext)
        {
            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                await httpContext.Response.WriteAsync(
                    @"<html>
                    <head><title>Index</title></head>
                    <body>" +
                        $"<h3>Welcome {httpContext.User.Identity.Name}</h3>" +
                        @"<a href='Account/Logout'>Sign Out</a>
                    </body></html>"
                    );
            }
            else
            {
                await httpContext.ChallengeAsync();
            }
        }

        public async Task SignInAsync(HttpContext httpContext)
        {
            if (string.Compare(httpContext.Request.Method, "GET") == 0)
            {
                await RenderLoginPageAsync(httpContext, null, null, null);
            }
            else
            {
                var userName = httpContext.Request.Form["userName"];
                var password = httpContext.Request.Form["password"];
                if (_accounts.TryGetValue(userName, out var pwd) && pwd == password)
                {
                    var identity = new GenericIdentity(userName, "Password");
                    var principal = new ClaimsPrincipal(identity);
                    await httpContext.SignInAsync(principal);
                }
                else
                {
                    await RenderLoginPageAsync(httpContext, userName, password, "Invalid user name or password!");
                }
            }
        }

        public async Task SignOutAsync(HttpContext httpContext)
        {
            await httpContext.SignOutAsync();
            httpContext.Response.Redirect("/");
        }

        public async static Task RenderLoginPageAsync(HttpContext httpContext, string userName, string password,
            string errorMessage)
        {
            httpContext.Response.ContentType = "text/html";
            await httpContext.Response.WriteAsync(
                @"<html>
                <head><title>Login</title></head>
                <body>
                    <form method='post'>" +
                            $"<input type='text' name='username' placeholder='User name' value ='{userName}'/>" +
                            $"<input type='password' name='password' placeholder='Password'  value ='{password}'/> " +
                            @"<input type='submit' value='Sign In' /></form>" +
                    $"<p style='color:red'>{errorMessage}</p>" +
                @"</body>
            </html>"
                );
        }
    }
}
