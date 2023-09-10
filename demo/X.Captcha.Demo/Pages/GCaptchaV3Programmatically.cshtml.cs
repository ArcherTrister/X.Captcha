﻿using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.RazorPages;

using X.Captcha;
using X.Captcha.G;

namespace XCaptcha.Demo.Pages;

public class GCaptchaV3Programmatically : PageModel
{
    private readonly IGCaptchaV3SiteVerify _siteVerify;

    public string Result { get; set; }

    public GCaptchaV3Programmatically(IGCaptchaV3SiteVerify siteVerify)
    {
        _siteVerify = siteVerify;
    }

    public async Task OnPostAsync(string token)
    {
        var response = await _siteVerify.Verify(new CaptchaSiteVerifyRequest
        {
            Response = token,
            RemoteIp = HttpContext.Connection.RemoteIpAddress?.ToString()
        });

        Result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }
}
