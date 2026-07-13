using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Compartilhado;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
