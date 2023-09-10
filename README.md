# X.HCaptcha

hCaptcha for ASP NET Core 6-7.0

# Install-Package

```
Install-Package X.HCaptcha
```

# hCaptcha

### startup

```
services.AddHCaptcha(x =>
{
    x.VerifyBaseUrl = "https://hcaptcha.com/"; // Default: https://api.hcaptcha.com/
    x.SiteKey = "your_site_key";
    x.SiteSecret = "your_site_secret";
});
```

### razor page(checkbox mode)

```
@addTagHelper *, X.HCaptcha

<hcaptcha-script />

<script>
    function callback(token) {
        document.getElementById("token").value = token;
    }
</script>

<form method="POST">
    <input id="token" name="token" type="text" />
    <input id="submit" type="submit" value="submit" />
</form>

<hcaptcha-div callback="callback" />
```

### razor page(invisible mode)

```
@addTagHelper *, X.HCaptcha

<script>
    function onload() {
        hcaptcha.execute();
    }

    function callback(token) {
        document.getElementById("token").value = token;
    }
</script>

<hcaptcha-script onload="onload" />

<form method="POST">
    <input id="token" name="token" type="text" />
    <input id="submit" type="submit" value="submit" />
</form>

<hcaptcha-div callback="callback" size="invisible" />
```

### razor page(invisible mode)

```
@addTagHelper *, X.HCaptcha

<script>
    function callback(token) {
        document.getElementById("token").value = token;
        document.getElementById("demo-form").submit();
    }
</script>

<hcaptcha-script  />

<form id="demo-form" method="POST">
    <input id="token" name="token" type="text" />
    <button hcaptcha-callback="callback" hcaptcha-size="invisible">Submit</button>
</form>
```

### razor page model

```
public class HCaptcha_CheckboxModel : PageModel
{
	private readonly IHCaptchaSiteVerify _siteVerify;

	public HCaptcha_CheckboxModel(IHCaptchaSiteVerify siteVerify)
	{
		_siteVerify = siteVerify;
	}

	public async Task OnPostAsync(string token)
	{
		var response = await _siteVerify.Verify(new reCAPTCHASiteVerifyRequest
		{
			Response = token,
			RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString()
		});

        /*
        https://docs.hcaptcha.com/#verify-the-user-response-server-side
        response:
        {
            "success": true|false,
            "challenge_ts": timestamp,  // timestamp of the challenge load (ISO format yyyy-MM-dd'T'HH:mm:ssZZ)
            "hostname": string,         // the hostname of the site where the hCaptcha was solved
            "error-codes": [...]        // optional
        }
        */
	}
}
```
