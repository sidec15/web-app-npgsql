using WebAppNpgsql.Dal;

namespace WebAppNpgsql.Context
{
  public static class DbInitializer
  {
    public static void Initialize(DbAppContext context)
    {
      // Look for any students.
      if (context.Users.Any())
      {
        return;   // DB has been seeded
      }

      var users = new User[]
      {
        new User{Name="user1-name",Surname="user1-surname",Age=10,HomeLocation=new NetTopologySuite.Geometries.Point(1,1)},
        new User{Name="user2-name",Surname="user2-surname",Age=20,HomeLocation=new NetTopologySuite.Geometries.Point(2,2)},
      };

      context.Users.AddRange(users);
      context.SaveChanges();

    }
  }
}
