using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.Controllers;
public class AIController : Controller
{
    public IActionResult CreateRecipeWithOpenAI()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipeWithOpenAI(string prompt)
    {
        // Burada projeyi kullanan kişinin, kendi OpenAI API anahtarıni girmelidir
        var apiKey = "YOUR_API_KEY_HERE";

        using var client = new HttpClient();

        // API anahtarını Authorization header olarak eklenir
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

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
                    content = "Sen bir restoran için yemek önerilerini yapan bir yapay zeka aracısın. Amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak."
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
        var response = await client.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestData);

        // API’den gelen cevap başarılıysa
        if (response.IsSuccessStatusCode)
        {
            // JSON cevabını OpenAIResponse sınıfına dönüştürülür
            var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

            // İlk cevabın içeriği alınır
            var content = result.choices?[0]?.message?.content;

            // ViewBag ile view tarafına tarifi gönderilir
            ViewBag.recipe = content;
        }
        else
        {
            // Hata durumunda HTTP status kodunu ekrana yazdırılır
            ViewBag.recipe = $"Bir hata oluştu: {response.StatusCode}";
        }

        return View();
    }

    // API’den gelecek veriyi karşılamak için model sınıfları
    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; }
    }

    public class Choice
    {
        public Message message { get; set; }
    }

    public class Message
    {
        // "system", "user" veya "assistant"
        public string role { get; set; }

        // AI’nin verdiği cevap
        public string content { get; set; }
    }
}
