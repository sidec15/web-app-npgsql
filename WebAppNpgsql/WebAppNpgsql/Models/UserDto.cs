using NetTopologySuite.Geometries;

namespace WebAppNpgsql.Dal
{
  public class UserDto
  {

    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string HomeLocation { get; set; }

  }
}
