using Microsoft.AspNetCore.Mvc;
using CurrencyConverterApplication.Models;
using System.Threading.Tasks;
using CurrencyConverterApplication.Models;

public class CurrencyConverterController : Controller
{
    private readonly CurrencyConverterService _converterService;

    public CurrencyConverterController(CurrencyConverterService converterService)
    {
        _converterService = converterService;
    }

    public IActionResult Index()
    {
        // Ensure you pass a new instance of CurrencyConverterModel to the view
        return View(new CurrencyConverterModel());
    }

    [HttpPost]
    public async Task<JsonResult> Convert(CurrencyConverterModel model, string apiKey)
    {
        // You can now use the apiKey to verify or log if needed
        if (string.IsNullOrEmpty(apiKey) || apiKey != "cc697982a80a8386e462d")
        {
            return Json(new { error = "Invalid API key" });
        }

        if (ModelState.IsValid)
        {
            // Perform the conversion using your service
            var result = await _converterService.ConvertCurrency(model.FromCurrency, model.ToCurrency, model.Amount);
            return Json(new { convertedAmount = result }); // Return the converted amount as JSON
        }

        return Json(new { convertedAmount = 0 }); // Return 0 if there's an error or invalid data
    }

}
