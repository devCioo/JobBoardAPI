﻿using System.Diagnostics;

namespace JobBoardAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopWatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var requestTime = _stopWatch.ElapsedMilliseconds;
            if (requestTime / 1000 > 4)
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path}] took {requestTime} ms";
                _logger.LogInformation(message);
            }
        }
    }
}
