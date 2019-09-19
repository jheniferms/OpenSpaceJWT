using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using OpenSpace.Sevices.Implementation;
using OpenSpace.Sevices.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace OpenSpace
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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Configurando o serviço de documentação do Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "Galeria de Filmes",
                        Version = "v1",
                        Description = "Exemplo de API utilizando JWT",
                        Contact = new Contact
                        {
                            Name = "Jhenifer M. Santos",
                            Url = "https://github.com/jheniferms"
                        }
                    });

                // Adicionando o XML
                string pathApplication = PlatformServices.Default.Application.ApplicationBasePath;
                string nameApplication = PlatformServices.Default.Application.ApplicationName;
                string pathXmlDoc = Path.Combine(pathApplication, $"{nameApplication}.xml");

                options.IncludeXmlComments(pathXmlDoc);
            });

            services.AddSingleton<IMovieService, MovieService>();
            services.AddSingleton<IActorService, ActorService>();
            services.AddSingleton<ILoginService, LoginService>();

            //Passo 2 - Configuração do serviço de autenticação JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, //validar o servidor que gera o token
                        ValidateAudience = true, //validar o destinatirio 
                        ValidateLifetime = true, //validar se o token não expirou e se o key é valida
                        ValidateIssuerSigningKey = true, //validar a assinatura
                        ValidIssuer = Configuration["Jwt:Issuer"], //emissor
                        ValidAudience = Configuration["Jwt:Audience"], //publico alvo
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])) //chave
                    };
                });
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

            //Passo 3 - Disponibilizar o serviço de autenticação 
            app.UseAuthentication();

            app.UseMvc();

            // Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "Galeria de Filmes");

                c.RoutePrefix = string.Empty;
            });

        }
    }
}
