using System.Text;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ApiProjectCamp.WebUI.Dtos.ProductDtos;
using ApiProjectCamp.WebUI.Dtos.CategoryDtos;

namespace ApiProjectCamp.WebUI.Controllers;

public class ProductController : Controller
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IActionResult> ProductList()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Products/ProductListWithCategory");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultProductDto>>(data);
            return View(result);
        }
        return View();
    }

    private async Task LoadCategories()
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync("https://localhost:44392/api/Categories");
        var data = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var dataC = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(dataC);
            List<SelectListItem> category = (from x in result
                                             select new SelectListItem
                                             {
                                                 Text = x.CategoryName,
                                                 Value = x.CategoryId.ToString()
                                             }).ToList();
            ViewBag.category = category;
        }
        else
        {
            ViewBag.category = new List<SelectListItem>();
        }
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        await LoadCategories();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductDto cpdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(cpdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Products/CreateProductWithCategory", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductList");
        }
        return View();
    }

    public async Task<IActionResult> DeleteProduct(int id)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.DeleteAsync($"https://localhost:44392/api/Products?id={id}");
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        await LoadCategories();

        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Products/GetProduct?id={id}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<UpdateProductDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProduct(UpdateProductDto updto)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategories();
            return View(updto);
        }

        var client = _httpClientFactory.CreateClient();
        var data = JsonConvert.SerializeObject(updto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Products/UpdateProductWithCategory", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("ProductList");
        }
        await LoadCategories();
        return View(updto);
    }
}
