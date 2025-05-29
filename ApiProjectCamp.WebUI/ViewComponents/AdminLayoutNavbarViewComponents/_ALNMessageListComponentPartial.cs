using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.MessageDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _ALNMessageListComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ALNMessageListComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Messages/MessageListByIsReadFalse");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultMessageByIsReadFalseDto>>(data);
            return View(result);
        }
        return View();
    }
}
