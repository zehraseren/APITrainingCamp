using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.AboutDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class AboutController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public AboutController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> AboutList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Abouts");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultAboutDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateAbout()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAbout(CreateAboutDto cadto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cadto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Abouts", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("AboutList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteAbout(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Abouts/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("AboutList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateAbout(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Abouts/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateAboutDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAbout(UpdateAboutDto uadto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(uadto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Abouts", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("AboutList");
        }
        return View();
    }
}
