using Microsoft.AspNetCore.Mvc;

namespace FirstBank.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}