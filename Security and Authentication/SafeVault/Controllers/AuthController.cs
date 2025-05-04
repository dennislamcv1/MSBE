using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("admin")]
public class AdminController : ControllerBase
{
    [HttpGet("dashboard")]
    [Authorize(Roles = "Admin")]
    public IActionResult GetAdminDashboard()
    {
        return Ok("Welcome Admin.");
    }
}


[ApiController]
[Route("vault")]
public class VaultController : ControllerBase
{
    [HttpGet("data")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult GetSecureData()
    {
        return Ok("Confidential Vault Content");
    }
}
