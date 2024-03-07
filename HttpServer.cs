
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;

internal class HttpServer : TcpServer
{
    private string[] SupportedMethods =
    {
        "GET"
    };

    private Encoding DefaultEncoding = Encoding.ASCII;
    private string ServerHttpVersion = "HTTP/1.1";

    public HttpServer(IPAddress Ip, ushort PortNumber, ILogger Logger) : base(Ip, PortNumber, Logger)
    {

    }

    protected override byte[] ProcessRequest(byte[] Bytes)
    {
        string RequestString = DefaultEncoding.GetString(Bytes);
        Logger.LogInformation($"{nameof(RequestString)}: {RequestString}");

        var ParsedRequest = new HttpRequest(RequestString);

        if (string.IsNullOrWhiteSpace(ParsedRequest.RequestUri))
        {
            throw new ArgumentNullException("Request URI cannot by null or whitespace");
        }

        if (!SupportedMethods.Contains(ParsedRequest.Method))
        {
            return DefaultEncoding.GetBytes(HttpResponse.NotImplemented(ServerHttpVersion).ToString());
        }

        if (ParsedRequest.RequestUri == "/")
        {
            return DefaultEncoding.GetBytes(HttpResponse.Ok(ServerHttpVersion).ToString());
        }

        if (ParsedRequest.RequestUri.ToLowerInvariant().StartsWith("/echo/"))
        {   
            var Response = HandleEcho(ParsedRequest.RequestUri);
            
            var httpResponse = HttpResponse.Ok(ServerHttpVersion,
                new Dictionary<string, string>
                {
                    { "Content-Type", "text/plain"},
                    { "Content-Length", Response.Length.ToString() ?? "0"}
                }, Response);

                var ResponseAsString = httpResponse.ToString();
            return DefaultEncoding.GetBytes(ResponseAsString);
        }

        if (ParsedRequest.RequestUri.ToLowerInvariant().StartsWith("/user-agent"))
        {
            var UserAgent = ParsedRequest.Headers["User-Agent"].Trim();
            var Response = HttpResponse.Ok(ServerHttpVersion, new Dictionary<string, string>()
            {
                { "Content-Type", "text/plain"},
                { "Content-Length", UserAgent.Length.ToString() ?? "0"}
            }, UserAgent);
            return DefaultEncoding.GetBytes(Response.ToString());
        }

        return DefaultEncoding.GetBytes(HttpResponse.NotFound(ServerHttpVersion).ToString());
    }

    string HandleEcho(string RequestUri)
    {
        var RecievedEcho = RequestUri["/echo/".Length..];
        return RecievedEcho;
    }
}