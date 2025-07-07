using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.WhyChooseYummyDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class WhyChooseYummyController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public WhyChooseYummyController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> WhyChooseYummyList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Services");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultWhyChooseYummyDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateWhyChooseYummy()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateWhyChooseYummy(CreateWhyChooseYummyDto cwcdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cwcdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Services", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("WhyChooseYummyList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteWhyChooseYummy(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Services?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("WhyChooseYummyList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateWhyChooseYummy(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Services/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateWhyChooseYummyDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateWhyChooseYummy(UpdateWhyChooseYummyDto uwcdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(uwcdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Services", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("WhyChooseYummyList");
        }
        return View();
    }
}
