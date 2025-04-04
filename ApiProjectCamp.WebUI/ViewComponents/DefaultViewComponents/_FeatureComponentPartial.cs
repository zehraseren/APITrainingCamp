using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.ViewComponents.DefaultViewComponents;

public class _FeatureComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}