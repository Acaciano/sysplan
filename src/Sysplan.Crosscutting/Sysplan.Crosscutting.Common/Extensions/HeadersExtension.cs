using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Sysplan.Crosscutting.Common.Extensions
{
    public static class HeadersExtension
    {
        public static object GetHeader(this IHttpContextAccessor httpContextAccessor, string key)
        {
            if (httpContextAccessor != null && httpContextAccessor.HttpContext != null)
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers.FirstOrDefault(x => x.Key == key);

                if (!string.IsNullOrEmpty(headers.Key) && !string.IsNullOrEmpty(headers.Value))
                {
                    if (!string.IsNullOrEmpty(headers.Value))
                        return headers.Value;
                }
            }

            return null;
        }
    }
}
