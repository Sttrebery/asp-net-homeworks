using homework_5.Configurations;
using Microsoft.Extensions.Options;

namespace homework_5.Services;

public class ApplicationInfoService(IOptionsSnapshot<ApplicationOptions> appOptions)
{
    private readonly ApplicationOptions _appOptions = appOptions.Value;

    public string GetApplicationInfo()
    {
        return $"Application Name: {_appOptions.Name}, Version: {_appOptions.Version}, MaxItems: {_appOptions.MaxItems}";
    }
}