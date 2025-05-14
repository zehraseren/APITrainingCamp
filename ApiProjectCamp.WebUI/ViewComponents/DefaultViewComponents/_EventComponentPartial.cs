using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.EventsDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _EventComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _EventComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient("");
        var response = await client.GetAsync("https://localhost:44392/api/Events");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultEventDto>>(data);
            return View(result);
        }
        return View();
    }
}
