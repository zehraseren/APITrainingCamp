using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using ApiProjectCamp.WebUI.Dtos.MessageDtos;

using SystemJson = System.Text.Json;
using NewtonsoftJson = Newtonsoft.Json;
using static ApiProjectCamp.WebUI.Controllers.AIController;

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
            var result = NewtonsoftJson.JsonConvert.DeserializeObject<List<ResultMessageDto>>(data);
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
    public async Task<IActionResult> CreateMessage(CreateMessageDto cmdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = NewtonsoftJson.JsonConvert.SerializeObject(cmdto);
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
            var result = NewtonsoftJson.JsonConvert.DeserializeObject<UpdateMessageDto>(data);
            return View(result);
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessage(UpdateMessageDto ucdto)
    {
        var client = _httpClientFactory.CreateClient();
        var data = NewtonsoftJson.JsonConvert.SerializeObject(ucdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PutAsync($"https://localhost:44392/api/Messages/", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> AnswerMessageWithOpenAI(int id, string prompt)
    {
        var client = _httpClientFactory.CreateClient();
        var response = await client.GetAsync($"https://localhost:44392/api/Messages/{id}");
        var data = await response.Content.ReadAsStringAsync();
        var result = NewtonsoftJson.JsonConvert.DeserializeObject<GetMessageByIdDto>(data);

        // Burada projeyi kullanan kişinin, kendi OpenAI API anahtarıni girmelidir
        var apiKey = "YOUR_API_KEY_HERE";

        using var clientAI = new HttpClient();

        // API anahtarını Authorization header olarak eklenir
        clientAI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        // OpenAI API'sine gönderecgönderilen istek verisi (JSON formatında)
        var requestData = new
        {
            // Kullanılacak OpenAI modeli
            model = "gpt-3.5-turbo",

            // ChatGPT’ye gönderilecek mesaj dizisi
            messages = new[]
            {
                new {
                    // Sistem mesajı: AI'ya rolünü açıklanır
                    role = "system",
                    content = "Sen bir restoran için kullanıcıların göndermiş oldukları mesajları detaylı ve olabilidğince olumlu, müşteri memnuniyetini gözeten cevaplar veren bir yapay zeka aracısın. Amacımız kullanıcı tarafından gönderilen mesajlara en olumlu ve mantıklı cevapları sunabilmek."
                },
                new {
                    // Kullanıcı mesajı: prompt değişkeninde gelen veri
                    role = "user",
                    content = prompt
                }
            },
            // Cevapların yaratıcılık seviyesi (0-1 arası)
            temperature = 0.5,
        };

        // OpenAI API'sine POST isteği gönderilir
        var responseAI = await clientAI.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

        // API’den gelen cevap başarılıysa
        if (responseAI.IsSuccessStatusCode)
        {
            // JSON cevabını OpenAIResponse sınıfına dönüştürülür
            var resultAI = await responseAI.Content.ReadFromJsonAsync<OpenAIResponse>();

            // İlk cevabın içeriği alınır
            var content = resultAI.choices?[0]?.message?.content;

            // ViewBag ile view tarafına tarifi gönderilir
            ViewBag.answerAI = content;
        }
        else
        {
            // Hata durumunda HTTP status kodunu ekrana yazdırılır
            ViewBag.answerAI = $"Bir hata oluştu: {responseAI.StatusCode}";
        }

        return View(result);
    }

    public PartialViewResult SendMessage()
    {
        return PartialView();
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(CreateMessageDto cmdto)
    {
        var clientAI = new HttpClient();
        var apiKey = "YOUR_API_KEY_HERE";
        clientAI.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        try
        {
            var translateRequestBody = new
            {
                inputs = cmdto.MessageDetails,
            };

            var translateJson = SystemJson.JsonSerializer.Serialize(translateRequestBody);
            var transelateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

            var translateResponse = await clientAI.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", transelateContent);

            var translateResponseString = await translateResponse.Content.ReadAsStringAsync();

            string englishText = cmdto.MessageDetails;
            if (translateResponseString.TrimStart().StartsWith("["))
            {
                var translateDoc = SystemJson.JsonDocument.Parse(translateResponseString);
                englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                //ViewBag.englishText = englishText;
            }

            var toxicRequestBody = new
            {
                inputs = englishText,
            };

            var toxicJson = SystemJson.JsonSerializer.Serialize(toxicRequestBody);
            var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
            var toxicResponse = await clientAI.PostAsync("https://api-inference.huggingface.co/models/unitary/toxic-bert", toxicContent);
            var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

            if (toxicResponseString.TrimStart().StartsWith("["))
            {
                var toxicDoc = SystemJson.JsonDocument.Parse(toxicResponseString);
                foreach (var item in toxicDoc.RootElement.EnumerateArray())
                {
                    string label = item.GetProperty("label").GetString();
                    double score = item.GetProperty("score").GetDouble();

                    if (score > 0.5)
                    {
                        cmdto.Status = "Toksik Mesaj";
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(cmdto.Status))
            {
                cmdto.Status = "Normal Mesaj";
            }
        }
        catch (Exception ex)
        {
            cmdto.Status = "Onay bekliyor";
            throw;
        }

        var client = _httpClientFactory.CreateClient();
        cmdto.SendDate = DateTime.Now;
        var data = NewtonsoftJson.JsonConvert.SerializeObject(cmdto);
        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:44392/api/Messages", content);
        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("MessageList");
        }
        return View();
    }
}
