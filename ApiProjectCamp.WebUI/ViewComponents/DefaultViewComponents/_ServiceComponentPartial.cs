using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.ServiceDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _ServiceComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ServiceComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Services");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultServiceDto>>(data);
            return View(result);
        }
        return View();
    }
}
