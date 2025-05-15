using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.ChefDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _ChefComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ChefComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Chefs");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultChefDto>>(data);
            return View(result);
        }
        return View();
    }
}
