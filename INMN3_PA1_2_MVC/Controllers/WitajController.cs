using Microsoft.AspNetCore.Mvc;
using INMN3_PA1_2_MVC.Models;

namespace INMN3_PA1_2_MVC.Controllers;
public class WitajController : Controller
{
    public IActionResult Index(int? id)
    {
        ViewData["id"] = id;
        return View();
    }
    public IActionResult Witaj(string? imię, int? id)
    {
        //nie zalecane
        /*return View(
            (Imię: imię, Ilość: id)
            );*/
        return View(
            new WitajViewModel() { Ilość = id, Imię = imię }
            );
    }
}
