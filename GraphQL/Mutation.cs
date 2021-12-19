using System.Threading.Tasks;
using CommanderGQL.Data;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;

namespace CommanderGQL.GraphQL
{
  public class Mutation
  {
    [UseDbContext(typeof(AppDbContext))]
    public async Task<AddPlatformPayload> AddPlatformAsync(AddPlatformInput input,
      [ScopedService] AppDbContext context)
    {
      var platform = new Platform
      {
        Name = input.Name
      };

      context.Platforms.Add(platform);
      await context.SaveChangesAsync();

      return new AddPlatformPayload(platform);
    }
  }
}
