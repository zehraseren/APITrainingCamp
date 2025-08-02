using ApiProjectCamp.WebUI.Dtos.ChefDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ApiProjectCamp.WebUI.Controllers;

public class ChefController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ChefController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ChefList()
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

    [HttpGet]
    public IActionResult CreateChef()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateChef(CreateChefDto ccdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ccdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Chefs", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ChefList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteChef(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Chefs?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ChefList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateChef(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Chefs/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateChefDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateChef(UpdateChefDto ucdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ucdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Chefs/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ChefList");
        }
        return View();
    }
}
