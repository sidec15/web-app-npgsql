using NetTopologySuite.Geometries;
using NetTopologySuite.IO;

namespace WebAppNpgsql.Extensions
{
  public static class GeometryExtension
  {
    public static string ToWKT(this Geometry geometry)
    {
      return geometry.ToWKT(4);
    }

    public static string ToWKT(this Geometry geometry, int outputDimension)
    {
      WKTWriter writer = new(outputDimension);
      return writer.Write(geometry);
    }

  }
}