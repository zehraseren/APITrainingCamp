using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.ContactDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class ContactController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ContactController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ContactList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Contacts");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultContactDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateContact()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateContact(CreateContactDto cadto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cadto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Contacts", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ContactList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteContact(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Contacts/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ContactList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateContact(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Contacts/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateContactDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateContact(UpdateContactDto uadto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(uadto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Contacts", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ContactList");
        }
        return View();
    }
}
