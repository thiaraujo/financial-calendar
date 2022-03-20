using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Middleware.Messages;

public class ContentLocalizer
{
    private readonly IStringLocalizer _localizer;

    public ContentLocalizer(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create("SharedResource", assemblyName.Name);
    }

    public LocalizedString GetLocalizedHtmlString(string key)
    {
        return _localizer[key];
    }
}