using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.FeatureDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class FeatureController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public FeatureController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> FeatureList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Features");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultFeatureDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateFeature()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateFeature(CreateFeatureDto cfdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cfdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Features", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("FeatureList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteFeature(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Features/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("FeatureList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateFeature(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Features/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateFeatureDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateFeature(UpdateFeatureDto ufdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ufdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Features/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("FeatureList");
        }
        return View();
    }
}
