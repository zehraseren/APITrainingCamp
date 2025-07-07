using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.TestimonialDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class TestimonialController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TestimonialController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> TestimonialList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Testimonials");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultTestimonialDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateTestimonial()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto ccdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ccdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Testimonials", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("TestimonialList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteTestimonial(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Testimonials?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("TestimonialList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateTestimonial(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Testimonials/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateTestimonialDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto ucdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ucdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Testimonials/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("TestimonialList");
        }
        return View();
    }
}

