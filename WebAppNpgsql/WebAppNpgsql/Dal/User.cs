using NetTopologySuite.Geometries;

namespace WebAppNpgsql.Dal
{
  public class User
  {

    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public Point HomeLocation { get; set; }

  }
}
