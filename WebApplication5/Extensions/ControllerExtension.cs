using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Extensions;

public static class ControllerExtension
{
    private const string ControllerSuffix = "Controller";

    public static string GetControllerName<TController>(this Controller controller) where TController : Controller
    {
        string controllerName = typeof(TController).Name;

        if (controllerName.EndsWith(ControllerSuffix))
        {
            controllerName = controllerName.Substring(0, "Controller".Length);
        }

        return controllerName;
    }
}