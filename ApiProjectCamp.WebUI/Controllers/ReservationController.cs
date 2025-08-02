using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.ReservationDtos;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace ApiProjectCamp.WebUI.Controllers;

public class ReservationController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ReservationController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ReservationList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Reservations");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultReservationDto>>(data);
            return View(result);
        }
        return View();
    }

    [HttpGet]
    public IActionResult CreateReservation()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(CreateReservationDto crdto)
    {
        DateTime reservationDateTime = crdto.ReservationDate + crdto.ReservationHour;

        var apiPayload = new
        {
            nameSurname= crdto.NameSurname,
            email = crdto.Email,
            phoneNumber = crdto.PhoneNumber,
            reservationDateTime = reservationDateTime,
            personCount = crdto.PersonCount,
            message = crdto.Message,
            reservationStatus = crdto.ReservationStatus
        };

        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(apiPayload);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Reservations", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ReservationList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteReservation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Reservations/{id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ReservationList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateReservation(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Reservations/{id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateReservationDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReservation(UpdateReservationDto urdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(urdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Reservations", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ReservationList");
        }
        return View();
    }
}
