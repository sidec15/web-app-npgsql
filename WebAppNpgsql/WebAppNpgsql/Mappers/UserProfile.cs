using AutoMapper;
using WebAppNpgsql.Dal;

namespace WebAppNpgsql.Mappers
{
  public class UserProfile : Profile
  {
    public UserProfile()
    {
      CreateMap<User, UserDto>();
    }
  }
}
