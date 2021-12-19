using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using GraphiQl;
using GraphQL.Server.Ui.Voyager;

namespace CommanderGQL
{
  public class Startup
  {
    private readonly IConfiguration Configuration;

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<AppDbContext>(
        opt => opt.
          UseSqlServer(Configuration.GetConnectionString("CommandConStr"))
      );

      services
        .AddGraphQLServer()
        .AddQueryType<Query>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseGraphiQl();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapGraphQL();
      });

      app.UseGraphQLVoyager(new GraphQLVoyagerOptions());

    }
  }
}
