using System.Net;
using Microsoft.Extensions.Logging;

using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
ILogger logger = factory.CreateLogger("Program");
var Server = new HttpServer(IPAddress.Any, 4221, logger);
Server.Start();