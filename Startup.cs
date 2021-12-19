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
using Microsoft.Extensions.Logging;

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
      services.AddPooledDbContextFactory<AppDbContext>(
        opt => opt
          .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
          .UseSqlServer(Configuration.GetConnectionString("CommandConStr"))
      );

      services
        .AddLogging(builder => builder.AddConsole())
        .AddGraphQLServer()
        .AddQueryType<Query>()
        .AddProjections();
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

      app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
      {
        GraphQLEndPoint = "/graphql",
        Path = "/graphql-voyager"
      });

    }
  }
}
