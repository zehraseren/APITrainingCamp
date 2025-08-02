using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.ImageDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class GalleryController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GalleryController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ImageList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Images");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultImageDto>>(data);
            return View(result);
        }
        return View();
    }
}
