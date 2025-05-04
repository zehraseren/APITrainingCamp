using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.TestimonialDtos;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _TestimonialDefaultComponentPartial : ViewComponent
{
    private readonly IHttpClientFactory _httpClientFactory;

    public _TestimonialDefaultComponentPartial(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
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
}
