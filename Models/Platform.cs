using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotChocolate;

namespace CommanderGQL.Models
{
  [GraphQLDescription("Represents any command or service that has a command line interface")]
  public class Platform
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [GraphQLDescription("Represents a valid purchased license for a platform")]
    public string LicenseKey { get; set; }

    public ICollection<Command> Commands { get; set; } = new List<Command>();
  }
}
