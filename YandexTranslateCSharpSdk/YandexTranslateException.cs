using System;

namespace YandexTranslateCSharpSdk
{
    public class YandexTranslateException : Exception
    {
        public YandexTranslateException()
        {
        }

        public YandexTranslateException(string message)
            : base(message)
        {
        }

        public YandexTranslateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
