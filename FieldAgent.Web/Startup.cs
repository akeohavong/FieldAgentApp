using FieldAgent.Core.Interfaces.DAL;
using FieldAgent.DAL;
using FieldAgent.DAL.EF;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FieldAgent.MVC
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuer = true,
                         ValidateAudience = true,
                         ValidateLifetime = true,
                         ValidateIssuerSigningKey = true,

                         ValidIssuer = "http://localhost:2000",
                         ValidAudience = "http://localhost:2000",
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeyForSignInSecret@1234"))
                     };
                     services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
                 });
            var dbf = new DBFactory();
            services.AddControllers();
            services.AddTransient<IMissionRepository>(m => new MissionRepository(dbf));
            services.AddTransient<IAgentRepository>(a => new AgentRepository(dbf));
            services.AddTransient<IAliasRepository>(a => new AliasRepsitory(dbf));

            


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
