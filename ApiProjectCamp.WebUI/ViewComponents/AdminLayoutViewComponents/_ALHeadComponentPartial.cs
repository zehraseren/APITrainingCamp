using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.ViewComponents.AdminLayoutViewComponents;

public class _ALHeadComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
