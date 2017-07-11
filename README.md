# YandexTranslateCSharpSdk
YandexTranslateCSharpSdk is a .NET C# library providing access for Yandex Translate API:

https://tech.yandex.com/translate

Project includes C# library with wrapper methods for Yandex Translate API, Visual Studio unit tests for this library and WPF demo application for library.

## Prerequisites
Requires .NET Framework 4.0 or higher. It has also a .NET Core version. The library is based on HttpClient. To use it in .NET 4.0, use Nuget package https://www.nuget.org/packages/Microsoft.Net.Http, versions of .NET 4.5 and higher and .NET Core have HttpClient by default.

To run tests create key.txt in YandexTranslateCSharpSdkTests folder and put the key which you receive from Yandex here:

https://tech.yandex.com/keys/get/?service=trnsl

List of supported languages can be found here:

https://tech.yandex.com/translate/doc/dg/concepts/api-overview-docpage/

## Terms of Use
According to the Terms of Use for the Yandex Translate service, the text “Powered by Yandex.Translate” must be shown above or below the translation result, 
with a clickable link to the page http://translate.yandex.com/.

https://tech.yandex.com/translate/doc/dg/concepts/design-requirements-docpage/
