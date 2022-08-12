using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DemoApp.MvcApp.Models;

namespace DemoApp.MvcApp.Controllers;

public class FunctionController : Controller
{
    private readonly ILogger<FunctionController> _logger;
    private readonly PayloadCache _payloadCache;

    public FunctionController(ILogger<FunctionController> logger, PayloadCache payloadCache)
    {
        _logger = logger;
        _payloadCache = payloadCache;
    }

    public JsonResult Status() {
        Payload payload = new Payload{ Name = "", Version = "" };
        _payloadCache.Add(payload);

        return Json(new {foo="bar"});
    }

    [HttpPost]
    public JsonResult Renew([FromBody] Payload payload) {
        _payloadCache.Add(payload);

        return Json(new {aaa="bbb"});
    }
}