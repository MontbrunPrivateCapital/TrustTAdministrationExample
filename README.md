# Introduction 

This is the Trustt Admin API Developer SDK's official documentation.

Using this tool, you can do every kind of administration task in your trustt account.

You may also see our swagger documentation, to test endpoints and see data format. Check out
[https://trustt-admin-api.azurewebsites.net/index.html](https://trustt-admin-api.azurewebsites.net/index.html)

# Getting Started

- lack, define authentication's credential obtaining

First, instantiate Trustt SDK main object using a `TrusttAdminAPI` instance. Constructor expects a `AppSettings` as argument, this object contains all the API's client configuration. So, this mean, you could use in Dependency Injection like this.

```C#
var settings = 
    new AppSettings 
    { 
        ApiKey = "asd09ad098a0d89wn0r98c08",
        BasePath = "/",
        TenantId = "asdf" 
    }; 
```

Then instantiate main API's object, just use `AppSettings` as constructor.

```C#
    var _api =
        new TrusttAdminAPI(settings)
```

So, you can also instantiate it as dependency injection.

```json
// this is the appsettings.json
"TrusttAdmin":{
    "Host":"trustt-admin-api.azurewebsites.net",
    "Bearer" : "t0k3n3xample99777asdfipsumlorem"
}
```

```C# 
// this is your «services» section in Startup.cs
services.AddTransient(p =>
    Configuration.GetSection("TrusttAdmin").Get<AppSettings>());

services.AddTransient<TrusttAdminAPI>();
```

Once we have an instance of `TrusttAdminAPI`, we can proceed to query the api as high level object. 

# Response Object

Every request returns an IResponse object carrying response results and payload. The response implements the following interface.

```C#
public interface IResponse<T>
{
    bool Success { get; set; }
    string[] Errors { get; set; }
    T Data { get; set; }
}
```

Where `T` will holds model class with convenient data formating. 

If operation was complete successfully, `.Success` property wil be `true` and `.Data` will carry on object model with expected data.

If operation was not successfully, `.Success` property wil be `false` and `.Data` becomes `null`

# Endpoints

From now, we assume **_api** as a valid `TrusttAdminAPI` instance and we'll call it.

## 