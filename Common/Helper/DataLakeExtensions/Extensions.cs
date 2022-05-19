
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class Extensions
    {
        public static bool HasData<T>(this T data)
        {
            if (data != null) return true;
            return false;
        }
        public static bool HasData<T>(this List<T> Source)
        {
            if (Source == null) return false;

            return Source.Any();
        }
        public static string GetFileName(this string Source)
        {
            if (string.IsNullOrEmpty(Source)) return "";
            return Source.Split("/")[^1];
        }
        public static string AppendTimeStamp(this string fileName)
        {
            return string.Concat(
                Path.GetFileNameWithoutExtension(fileName),
                "-",
                DateTime.Now.ToString("yyyyMMddHHmmssfff"),
                Path.GetExtension(fileName)
                );
        }
    }
}
