using System;
using System.IO;

namespace QianShiChat.Server.Extensions
{
    public static class ArrayExtension
    {
        public static string Join(this Array array, char separator = ',')
        {
            return string.Join(separator, array);
        }

    }
}
