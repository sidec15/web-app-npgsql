using AutoMapper;
using NetTopologySuite.Geometries;
using WebAppNpgsql.Extensions;

namespace WebAppNpgsql.Mappers
{
  public class GeometryProfile : Profile
  {
    public GeometryProfile()
    {
      CreateMap<Geometry, string>().ConvertUsing(geom => geom == null ? null : geom.ToWKT());

      CreateMap<LineString, string>().ConvertUsing(geom => geom == null ? null : geom.ToWKT());

      CreateMap<Point, string>().ConvertUsing(geom => geom == null ? null : geom.ToWKT());

    }
  }
}
