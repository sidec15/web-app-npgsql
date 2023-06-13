using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using WebAppNpgsql.Context;
using WebAppNpgsql.Dal;

namespace WebAppNpgsql.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class UserController : ControllerBase
  {

    private readonly ILogger<UserController> _logger;
    private readonly IMapper _mapper;
    private readonly DbAppContext _dbContext;

    public UserController(ILogger<UserController> logger, IMapper mapper, DbAppContext dbContext)
    {
      _logger = logger;
      _mapper = mapper;
      _dbContext = dbContext;
    }



    /// <summary>
    /// Return info of all users
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult<UserDto[]> Get()
    {

      var users = _dbContext.Users.ToList();

      var dtos = _mapper.Map<UserDto[]>(users);

      return Ok(dtos);

    }
  }
}