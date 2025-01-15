using Microsoft.AspNetCore.Mvc;

namespace TheEmployeeAPI;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : Controller
{}