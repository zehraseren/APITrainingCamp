using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.YummyEventDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class YummyEventController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public YummyEventController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> YummyEventList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Events");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultYummyEventDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateYummyEvent()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateYummyEvent(CreateYummyEventDto cyedto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cyedto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Events", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("YummyEventList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Events?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("YummyEventList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateYummyEvent(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Events/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateYummyEventDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateYummyEvent(UpdateYummyEventDto uyedto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(uyedto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Events/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("YummyEventList");
        }
        return View();
    }
}
