using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace School_Management_System.MiddleWare
{
    public class LimitingRequest
    {
        private readonly RequestDelegate _next;

        // Maps an IP Address string to a specific ClientRequestTracker instance
        private static readonly ConcurrentDictionary<string, ClientRequestTracker> _clientTrackers = new();

        private const int MaxRequests = 5;
        private const int WindowSeconds = 20;

        public LimitingRequest(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string path = httpContext.Request.Path.Value?.ToLower() ?? "";

            // 1. Skip background assets that MVC pages load natively
            if (path.Contains("favicon.ico") ||
                path.Contains("__vs_browserlink") ||
                path.StartsWith("/css") ||
                path.StartsWith("/js") ||
                path.StartsWith("/lib"))
            {
                await _next(httpContext);
                return;
            }
            // 1. Identify the user by their IP Address
            var clientIp = httpContext.Connection.RemoteIpAddress?.ToString() ?? "UnknownClient";

            // 2. Get or create a unique tracking window for this specific IP address
            var tracker = _clientTrackers.GetOrAdd(clientIp, _ => new ClientRequestTracker());

            bool isRequestAllowed = false;

            // 3. Lock only this specific user's tracker (thread-safe, high-performance)
            lock (tracker)
            {
                var currentTime = DateTime.Now;

                if ((currentTime - tracker.WindowStart).TotalSeconds > WindowSeconds)
                {
                    // Reset window for this user
                    tracker.Count = 1;
                    tracker.WindowStart = currentTime;
                    isRequestAllowed = true;
                }
                else
                {
                    tracker.Count++;
                    if (tracker.Count <= MaxRequests)
                    {
                        isRequestAllowed = true;
                    }
                }
            }

            if (isRequestAllowed)
            {
                await _next(httpContext);
            }
            else
            {
                // Send back a proper global 429 page
                httpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                httpContext.Response.ContentType = "text/plain";
                await httpContext.Response.WriteAsync("Too many requests from your IP. Please wait 20 seconds.");
            }
        }
    }

    // A helper class to hold tracking data per client
    public class ClientRequestTracker
    {
        public int Count { get; set; }
        public DateTime WindowStart { get; set; } = DateTime.Now;
    }
}