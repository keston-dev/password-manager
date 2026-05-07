using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Controllers;

[Route("tools")]
public class ToolsController : Controller
{
    [Route("password-generator")]
    public ActionResult PasswordGenerator()
    {
        return View("PasswordGenerator");
    }
}