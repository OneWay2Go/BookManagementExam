namespace _6_modul_exam.Middlewares
{
    public class ServerTimeMiddleware
    {
        private readonly RequestDelegate _next;

        public ServerTimeMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Add("X-Server-Time", DateTime.UtcNow.ToString());

            await _next(context);
        }
    }
}
