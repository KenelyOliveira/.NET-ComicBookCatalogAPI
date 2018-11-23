using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibliotech.Repository.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Bibliotech.Configuration;
using Bibliotech.Facade;

namespace Bibliotech
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
            //essa opção para usar o banco de dados em memória é recente, e permite fazer todas as validações e testes
            //sem ter que usar um banco de dados físico =)
            services.AddDbContext<BibliotechContext>(opcao => opcao.UseInMemoryDatabase("BiblioTech"));


            //ao registrar um singleton, essa mesma instancia da classe será reaproveitada em toda a aplicação
            var tokenConfig = new TokenConfigurations();
            services.AddSingleton<TokenConfigurations>(tokenConfig);

            //services.Add(LoginFacade);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
