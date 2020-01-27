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

If operation was not successfully, `.Success` property wil be `false` and `.Data` becomes `null`. `.Error` property will container a list of errors involved if any.

# Endpoints

From now, we assume **_api** as a valid `TrusttAdminAPI` instance and we'll call it.

## Login

Login method proceed to get a valid login token. Internally, the instance will be fulfilled with token mechanism, so, you do not need to handle this, just perform a `Login()` request before any other.

With this endpoint we also learn the basic SDK workflow.

```C#

// load the model and fill your data
var credentials =
    new UserRegisterLoginRequest
    {
        Email = "youremail@domain.com,
        Password = "topsecretword"
    };

// perform the request to the api
var response = _api.Login(credentials);

// the following example will fetch your token
// or get the error if any
var data = 
    response.Success ?
    response.Data.AccessToken :
    response.Data.Error.First();

// example performing some code logic
if (response.Success)
    Console.WriteLine($"your token is {data}");
else
    Console.WriteLine($"request error: {data}");
```

## VerificationCode

Send a verification code to the user related in given email. When a new user arrives to the system, a verification code should be send. Should not confused with **TwoFactorGeneration**.

```C#
_api.VerificationCode(new EmailRequest {Email = "someuser@domain.com"});
```

## TwoFactorGeneration

Send a code toe user related in the given email. Usefully to perform some "sensible" operation where the user should confirm it.

WARNING! This endpoint expect a user's ID as argument. Should by a GUID but as string **not as Guid instance**.

```C#
_api.VerificationCode("cb4b6822-f4c4-40f3-a128-6c1b72549874");
```

## GetProfile

Returns full user profile by the given user' ID.

WARNING! This endpoint expect a user's ID as argument. Should by a GUID but as string **not as Guid instance**.

```C#
var totalAvalaibleBalance =
    _api
        .GetProfile("cb4b6822-f4c4-40f3-a128-6c1b72549874")
        .Data.Sources
        .Where(s => s.WalletType==0)
        .Select(s => s.Balance).Sum();
```

## ChangePassword

Change your current password by the given new password, yo should also provide new the old password.

```C#
var chpwr =
    new ChangePaswordRequest
    {
        NewPassword = "topsecretstrongpassword",
        OldPassword = "1234"
    };
```

API will accept any kind of password except and empty one, but remember you should always use strongest possible password. Check for https://passwordsgenerator.net/ in order to generate the best and secure passwords.


