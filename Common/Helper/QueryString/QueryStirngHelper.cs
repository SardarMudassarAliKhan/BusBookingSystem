using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Helper
{
    public static class QueryStringHelper
    {
        public static Dictionary<string, string> GetData(string requestBody)
        {
            return requestBody.Split('&')
                .Select(value => value.Split('='))
                .ToDictionary(pair => pair[0], pair => pair[1]);
        }
        public static string GetValue(this Dictionary<string, string> requestBody, string key)
        {
            return requestBody.ContainsKey(key) ? requestBody[key] : "";
        }
    }
}
