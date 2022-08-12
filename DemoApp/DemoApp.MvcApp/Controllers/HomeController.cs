using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoApp.MvcApp.Models;

namespace DemoApp.MvcApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PayloadCache _payloadCache;

    public HomeController(ILogger<HomeController> logger, PayloadCache payloadCache)
    {
        _logger = logger;
        _payloadCache = payloadCache;
    }

    public IActionResult Index()
    {
        HttpContext.Response.Headers.Add("Refresh", "5"); 
        return View(_payloadCache);
    }

    public IActionResult Reset()
    {
        _payloadCache.Clear();
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
