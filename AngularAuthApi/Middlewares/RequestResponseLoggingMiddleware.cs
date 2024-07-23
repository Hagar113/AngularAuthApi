namespace AngularAuthApi.Middlewares
{
    public class RequestResponseLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestResponseLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;

        //bkr2 el req body kter 
        request.EnableBuffering();
        var requestBody = await new StreamReader(request.Body).ReadToEndAsync();
        request.Body.Position = 0;
            //stream reader bkr2 bha req data
            // b3d m ykr2 yrg3 el stream l 0 34an el middle ware ykr2 eldata b3den



            //b5zn elstrem el2sly ll res body 
        var originalBodyStream = response.Body;
        using var newBodyStream = new MemoryStream(); //b5zn el res 
        response.Body = newBodyStream;

        
        await _next(context);

        
        response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        
        await LogRequestResponseAsync(request, requestBody, response, responseBody);

       
        await newBodyStream.CopyToAsync(originalBodyStream);
    }

    private async Task LogRequestResponseAsync(HttpRequest request, string requestBody, HttpResponse response, string responseBody)
    {
        var logFilePath = GetLogFilePath();
        var logEntry = $"Request: {request.Method} {request.Path}{request.QueryString}\n" +
                       $"Request Body:\n{requestBody}\n" +
                       $"Response Status: {response.StatusCode}\n" +
                       $"Response Body:\n{responseBody}\n" +
                       $"----------------------------------------------------\n";

        await File.AppendAllTextAsync(logFilePath, logEntry);
    }

    private string GetLogFilePath()
    {
        var today = DateTime.Now.ToString("yyyyMMdd");
        var logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Controllers", "log");

        if (!Directory.Exists(logFolderPath))
        {
            Directory.CreateDirectory(logFolderPath);
        }

        return Path.Combine(logFolderPath, $"{today}.txt");
    }

    }
}
