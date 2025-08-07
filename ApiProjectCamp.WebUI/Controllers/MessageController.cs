using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.MessageDtos;

namespace ApiProjectCamp.WebUI.Controllers;
public class MessageController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public MessageController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> MessageList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Messages");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultMessageDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateMessage()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(CreateMessageDto ccdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ccdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Messages", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Messages?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMessage(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Messages/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateMessageDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto ucdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(ucdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Messages/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AnswerMessageWithOpenAI(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Messages/{id}");
        var data = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<GetMessageByIdDto>(data);
        return View(result);
    }
}
