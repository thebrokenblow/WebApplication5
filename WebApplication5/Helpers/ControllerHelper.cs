using Microsoft.AspNetCore.Mvc;

namespace WebApplication5.Helpers;

public static class ControllerHelper
{
    public static string GetName<TController>() where TController : Controller
    {
        var type = typeof(TController);
        var nameController = type.Name;

        return nameController.Replace("Controller", "");
    }
}