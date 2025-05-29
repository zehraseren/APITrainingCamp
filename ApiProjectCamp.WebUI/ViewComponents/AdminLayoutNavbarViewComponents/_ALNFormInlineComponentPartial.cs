using Microsoft.AspNetCore.Mvc;

namespace ApiProjectCamp.WebUI.ViewComponents.AdminLayoutNavbarViewComponents;

public class _ALNFormInlineComponentPartial : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
