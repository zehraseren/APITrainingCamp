using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.NotificationDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _ALNNotificationComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _ALNNotificationComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Notifications/NotificationListByIsReadFalse");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultNotificationByIsReadFalseDto>>(data);
            return View(result);
        }
        return View();
    }
}
