using Microsoft.AspNetCore.Mvc;

namespace FirstBank.Controllers
{
    public class BankersController : Controller
    {

      public ActionResult Index()
      {
        return View();
      }

    }
}