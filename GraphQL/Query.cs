using System.Linq;
using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;

namespace CommandeGQL.GraphQL
{
  public class Query
  {
    public IQueryable<Platform> GetPlatform([Service] AppDbContext context)
    {
      return context.Platforms;
    }
  }
}
