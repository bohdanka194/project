using books.Data;
using Internal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using books;

namespace Internal
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                       .AddJwtBearer(options =>
                       {
                           options.RequireHttpsMetadata = false;
                           options.TokenValidationParameters = new TokenValidationParameters
                           {
                               // укзывает, будет ли валидироваться издатель при валидации токена
                               ValidateIssuer = true,
                               // строка, представляющая издателя
                               ValidIssuer = AuthOptions.ISSUER,

                               // будет ли валидироваться потребитель токена
                               ValidateAudience = true,
                               // установка потребителя токена
                               ValidAudience = AuthOptions.AUDIENCE,
                               // будет ли валидироваться время существования
                               ValidateLifetime = true,

                               // установка ключа безопасности
                               IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                               // валидация ключа безопасности
                               ValidateIssuerSigningKey = true,
                           };
                       });

            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CurrentContext>(options => options.UseSqlServer(connection));

            services.AddTransient<ICart, DbCart>(
                serviceProv => new DbCart(serviceProv.GetService<CurrentContext>(), Guid.NewGuid())
            );

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            //app.UseDeveloperExceptionPage();
            app.UseMiddleware<ExceptionMiddleware>();
            if (env.IsProduction())
            {
                app.UseHsts();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                try
                {
                    var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    var context = scope.ServiceProvider.GetService<CurrentContext>();

                    context.Database.Migrate();
                    Book[] seedItems = new Book[]
                    {
                        new FakeBook(new Guid("aebed534-3995-49fa-a050-6f8de4da22f1")),
                        new Book()
                        {
                            Pages = 178,
                            Title = "The Alchemist",
                            Author = "Paulo Coelho",
                            Image = "https://images-na.ssl-images-amazon.com/images/I/51Z0nLAfLmL.jpg",
                            Description = "A special 25th anniversary edition of the extraordinary" +
                                          " international bestseller, including a new Foreword by Paulo Coelho."+
                                          "Combining magic, mysticism, wisdom and wonder into an inspiring tale" +
                                          " of self-discovery, The Alchemist has become a modern classic, selling" +
                                          " millions of copies around the world and transforming the lives of" +
                                          " countless readers across generations.",
                            Price = 14
                        },
                        new Book()
                        {
                            Title = "Beyond Good and Evil",
                            Author = "Friedrich Nietzsche",
                            Image = "https://images-na.ssl-images-amazon.com/images/I/41jNG8cggML._SX331_BO1,204,203,200_.jpg",
                            Description = "Beyond Good and Evil: Prelude to a Philosophy of the Future is a book by philosopher Friedrich Nietzsche, first published in 1886. It draws on and expands the ideas of his previous work, Thus Spoke Zarathustra, but with a more critical and polemical approach. In Beyond Good and Evil, Nietzsche accuses past philosophers of lacking critical sense and blindly accepting dogmatic premises in their consideration of morality.",
                            Pages = 116,
                            Price = 67.9, 
                        }
                    };
                    if (context.Books.CountAsync().GetAwaiter().GetResult() == 0)
                    {
                        context.Books.AddRange(seedItems);
                    }
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database.");
                }
            }
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
