# YandexTranslateCSharpSdk

![alt YandexTranslateCSharpSdk icon](https://github.com/anovik/YandexTranslateCSharpSdk/blob/master/translate.png)

![alt Appveyor status](https://ci.appveyor.com/api/projects/status/t069512wi8cywvjl/branch/master?svg=true)

YandexTranslateCSharpSdk is a .NET C# library providing access for Yandex Translate API:

https://tech.yandex.com/translate

Project includes C# library with wrapper methods for Yandex Translate API, Visual Studio unit tests for this library and WPF demo application for library. Project also includes .NET Core 3.1 library, tests for it and ASP.NET Core demo application.

## Nuget

You can add .NET Framework library to your project using Nuget:

https://www.nuget.org/packages/YandexTranslateCSharpSdk

You can add .NET core library to your project using Nuget:

https://www.nuget.org/packages/YandexTranslateCoreSdk/

## How to use

```cs
YandexTranslateSdk wrapper = new YandexTranslateSdk();
wrapper.ApiKey = "theKeyYouGotFromYandexHere";
string englishText = "Hello, World";
string translatedText = await wrapper.TranslateText(englishText, "en-fr");
```

## Prerequisites
Requires .NET Framework 4.0 or higher or .NET Core. The library is based on HttpClient. To use it in .NET 4.0, use Nuget package https://www.nuget.org/packages/Microsoft.Net.Http, versions of .NET Framework 4.5 or higher and .NET Core have HttpClient by default.

To run tests get API key from Yandex:

https://tech.yandex.com/keys/get/?service=trnsl

and put it into the file key.txt in YandexTranslateCSharpSdkTests.

List of languages supported by Yandex.Translate API can be found here:

https://tech.yandex.com/translate/doc/dg/concepts/api-overview-docpage/

## Terms of Use
According to the Terms of Use for the Yandex Translate service, the text “Powered by Yandex.Translate” must be shown above or below the translation result, 
with a clickable link to the page http://translate.yandex.com/.

https://tech.yandex.com/translate/doc/dg/concepts/design-requirements-docpage/
