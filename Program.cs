using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");
var Server = new HttpServer(IPAddress.Any, 4221, logger);
Server.Start();


// using (Socket socket = Server.AcceptSocket())
// {
//     var RequestBuff = new byte[1024];
//     _ = socket.Receive(RequestBuff);
//     string RequestString = Encoding.ASCII.GetString(RequestBuff);

//     string Path = ExtractHttpPath(RequestString);
//     byte[] ResponseBuff = new byte[1024];

    // ResponseBuff = Path switch
    // {
    //     "/" => Encoding.ASCII.GetBytes("HTTP/1.1 200 Ok\r\n\r\n"),
    //     _ => ResponseBuff = Encoding.ASCII.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n"),
    // }

//     if (Path == "/")
//     {
//         ResponseBuff = Encoding.ASCII.GetBytes($"HTTP/1.1 200 Ok\r\n\r\n");
//         socket.Send(ResponseBuff);
//         return;
//     }
//     var LoweredPath = Path.ToLower();
//     string[] SplittedPath = LoweredPath.Split('/');
//     if (SplittedPath[1] == "echo")
//     {
//         string RecievedEcho = LoweredPath.Split("/echo/")[1];
//         ResponseBuff = Encoding.ASCII.GetBytes($"HTTP/1.1 200 Ok" +
//             $"\r\nContent-Type: text/plain" + 
//             $"\r\nContent-Length: {RecievedEcho.Length}" +
//             $"\r\n{RecievedEcho}");
//         socket.Send(ResponseBuff);
//         return;
//     }
//     ResponseBuff = Encoding.ASCII.GetBytes("HTTP/1.1 404 Not Found\r\n\r\n");
//     socket.Send(ResponseBuff);

// }

// string ExtractHttpPath(string RequestString)
// {
//     if (string.IsNullOrWhiteSpace(RequestString))
//     {
//         throw new ArgumentException(
//             $"'{nameof(RequestString)}' no puede ser nulo o contener espacios en blanco.", nameof(RequestString)
//         );
//     }

//     var RequestLines = RequestString.Split('\n');
//     Debug.Assert(RequestLines.Length > 0, "Http request debe tener al menos una línea");

//     var SplittedFirstLine = RequestLines[0].Split(' ');
//     Debug.Assert(SplittedFirstLine.Length == 3, "La línea inicial debe contar con al menos 3 espacios");

//     var Path = SplittedFirstLine[1];
//     Debug.Assert(!string.IsNullOrWhiteSpace(Path), "la ruta no debe estar en blanco");

//     return Path;
// }

