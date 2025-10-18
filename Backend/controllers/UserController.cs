using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController(UserService userService) : ControllerBase
{
    private readonly UserService _userService = userService;

}