using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class CurrencyConverterService
{
    private static readonly string apiUrl = "https://api.exchangerate-api.com/v4/latest/";

    public async Task<decimal> ConvertCurrency(string fromCurrency, string toCurrency, decimal amount)
    {
        var client = new HttpClient();
        var response = await client.GetStringAsync(apiUrl + fromCurrency);

        JObject exchangeRates = JObject.Parse(response);
        decimal exchangeRate = exchangeRates["rates"][toCurrency].Value<decimal>();

        return amount * exchangeRate;
    }
}
