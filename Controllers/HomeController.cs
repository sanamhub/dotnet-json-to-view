using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MvcApp.Models;
using MvcApp.ViewModels;

namespace MvcApp.Controllers;
public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) {
        _logger = logger;
    }

    public IActionResult Index() {
        try {
            var vm = GetVmFromJson();
            return View(vm ??= new List<JsonVm>());
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Error at {nameof(Index)}");
            return View();
        }
    }

    [HttpPost]
    public IActionResult Index(string productId) {
        try {
            var vm = GetVmFromJson();

            if (string.IsNullOrEmpty(productId)) {
                return View(vm ??= new List<JsonVm>());
            }
            else {
                if (int.TryParse(productId, out int prodId)) {
                    var filteredVm = vm
                        .Where(x => x.Provider.ProviderId == prodId)
                        .ToList();

                    ViewBag.ProductId = productId;

                    return View(filteredVm ??= new List<JsonVm>());
                }
                else {
                    ViewBag.error = "Invalid Input";
                }
            }
            return View(new List<JsonVm>());
        }
        catch (Exception ex) {
            _logger.LogError(ex, $"Error at {nameof(Index)}");
            return View(new List<JsonVm>());
        }
    }

    public IActionResult Privacy() {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region Private methods

    /// <summary>
    /// Get list of JsonVm from data.json which is at root directory
    /// </summary>
    /// <returns>List of JsonVm</returns>
    private static List<JsonVm>? GetVmFromJson() {
        string json = System.IO.File.ReadAllText("./data.json");
        return JsonSerializer.Deserialize<List<JsonVm>>(json);
    }

    #endregion
}
