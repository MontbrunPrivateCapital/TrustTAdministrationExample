# Introduction 

This is the Trustt Admin API Developer SDK's official documentation.

Using this tool, you can do every kind of administration task in your trustt account.

You may also see our swagger documentation, to test endpoints and see data format. Check out
[https://trustt-admin-api-test.azurewebsites.net/index.html](https://trustt-admin-api-test.azurewebsites.net/index.html)

# Getting Started

- lack, define authentication's credential obtaining

First, instantiate Trustt SDK main object using a `TrusttAdminAPI` instance. Constructor expects a `AppSettings` as argument, this object contains all the API's client configuration. So, this mean, you could use in Dependency Injection like this.

```C#
var settings = 
    new AppSettings 
    { 
        ApiKey = "asd09ad098a0d89wn0r98c08",
        BasePath = "https://trustt-admin-api-test.azurewebsites.net",
        TenantId = "asdf" 
    }; 
```

Then instantiate main API's object, just use `AppSettings` as constructor.

```C#
    var _api = new TrusttAdminAPI(settings)
```

So, you can also instantiate it as dependency injection.

```json
// this is the appsettings.json

// this section store api settings
"TrusttAdminAPI":{
    "BasePath":"https://trustt-admin-api-test.azurewebsites.net",
    "ApiKey" : "t0k3n3xample99777asdfipsumlorem",
    "TenantId": "asdf"
}
```

Configure AppSettings, then add the `TrusttAdminAPI` object as transient service.

```C# 
// this is your «services» section in Startup.cs

// bind the «TrusttAdminAPI» to «AppSettings» object
services.AddTransient(p =>
    Configuration.GetSection("TrusttAdminAPI").Get<AppSettings>());

// add «TrusttAdminAPI» as trandsient service using «ITrusttAdminAPI» interface
services.AddTransient<ITrusttAdminAPI, TrusttAdminAPI>();
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

If operation was not successfully, `.Success` property wil be `false`, then `.Data` becomes `null` and `.Error` property will contain a list of errors if any.

# Endpoints

From now, we assume there is a scoped variable named «**_api**» containing a valid `TrusttAdminAPI` instance.

For example, assume the following dependency injection:

```C#
public class TrusttService
{
    private readonly ITrusttAdminAPI _api;

    public TrusttService(ITrusttAdminAPI api)
    {
        _api = api;
    }

    public SomeClass SomeMethod()
    {
        // all the examples happens here
    }

}
```

## Login

Login method proceed to get a valid login token. Internally, the instance will be fulfilled with token mechanism, so, you do not need to handle this, just perform a `Login()` request before any other.

With this endpoint we also learn the basic SDK workflow.

```C#

// load the model and fill your data
var credentials =
    new UserRegisterLoginRequest
    {
        Email = "youremail@domain.com",
        Password = "topsecretword"
    };

// perform the request to the api
// get back response and set in a convenient variable
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

Sen a code to the user related in the given email. Usefully to perform some "sensible" operation where the user should confirm it.

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

Change your current password by the given new password, yo should also provide the old password.

```C#
var chpwr =
    new ChangePaswordRequest
    {
        OldPassword = "1234",
        NewPassword = "topsecretstrongpassword"
    };
```

API will accept any kind of password except and empty one, but remember you should always use strongest possible password. Check for https://passwordsgenerator.net/ in order to generate the best and secure passwords.

## ForgotPassword

Trigger a password forget procedure to the given email (as string) is there is a related user.

```C#
var request = _api.ForgotPassword("somebody@domain.com");

if (!request.Success)
    foreach(var e in request.Errors)
        Console.WriteLine(e);
```

## ResetPassword

Performs a password reset procedure, using the emailed code. Requirers all the field to by populated or will fail.

```C#
var pr = 
    new ResetPassword
    {
        Code = "0986", // code got by email
        Email = "user@domain.com", // user to be reset
        Password = "yourBrandNewPassword"
    };

_api.ResetPassword(pr);
```

## GetUserList

Get a full list of all your users.

```C#
var users = 
    _api.GetUserList()
        .Select(ur => ur.Data);
```

## GetActivitiesList

Get a list of activities related to the given user. Yo can also make some filtering options, for example, we are looking for activity related to `Coin` operations.

```C#

var user = "username@domain.com";

var filter =
    new ActivityQueryRequest
    {
        WalletAccountType = WalletAccountTypes.Coin
    };

var activities = 
    _api.GetActivitiesList(user,filter)
    .Select(r => r.Data);
```

Yo can use `ActivityQueryRequest` as filter to perform specific search. Checkout the properties `.InitDate` and `.FinishDate` to reduce search scope, setting dates will get a activity between given dates. If you set `InitDate` you should also set a `FinishDate`, it works in ranges.

You may also fetch all your activities using not arguments, we don't recommend this but you may need sometimes.

```C#
_api.GetActivitiesList()
```

## SetWhiteLabelSettings

Customize your white label colors.

You should use a `Colors` object, specifying all the related colors in properties as html or rgb code as string. You can use a [color selector](https://htmlcolorcodes.com/es/selector-de-color/) to generate and get code colors.

```C#
var whiteLabel = 
        new SettingsModel
        {
            Colors = new Colors
            {
                Primary = "#335BFF",
                Main = "#7133FF"
                ...
            }
        };

_api.SetWhiteLabelSettings(whiteLabel);
```

Ensure every color should starting with «#».

## GetUsersPendingApproval

Get a list of all request to be pending for approve.

```C#

// get all documentsId 
var documents = 
    _api.GetUsersPendingApproval(whiteLabel)
    .Select(d => d.Data.Id);
```

## ApproveUserVerification

Mark an request as verified by its ID.

WARNING! This endpoint expect a user's ID as argument. Should by a GUID but as string **not as Guid instance**.

```C#
_api.ApproveUserVerification("cb4b6822-f4c4-40f3-a128-6c1b72549874");
```

## RejectUserVerification

Reject a user verification, refusing user account creating and disabling that email to login.

```C#
var note = 
    new NoteModel
    {
        Data = "KYC issue, please double check provided info"
    };

_api.ApproveUserVerification("cb4b6822-f4c4-40f3-a128-6c1b72549874",note);
```

## GetBase64ImageFromDocument

Get documents uploaded by users as base64 encoding. You should provide a document's ID, see the `GetUsersPendingApproval` endpoint.

```C#
var picture =
    _api.ApproveUserVerification("cb4b6822-f4c4-40f3-a128-6c1b72549874")
    .Data.Picture;
```

## GetCountrySpecs

Get required data fields to the given country. Each country has its own requirements to fulfill KYC demands.

```C#
var individualVF =
    _api.GetCountrySpecs("ES").Data
    .verificationFields.individual
    .Select(i => i.minimum);
```

Country code is expected as  ISO-3166 codes. You can check countries and codes at [https://www.iban.com/country-codes](https://www.iban.com/country-codes).


