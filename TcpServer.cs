using System.Net;
using System.Net.Sockets;
using Microsoft.Extensions.Logging;

internal abstract class TcpServer
{    
    public IPAddress Ip { get; }
    public ushort PortNumber { get; }
    public ILogger Logger { get; }

    protected int MaxRecieveBytes { get; set; }
    public bool ShouldStop = false;

    public TcpServer(IPAddress Ip, ushort PortNumber, ILogger Logger)
    {
        this.Ip = Ip;
        this.PortNumber = PortNumber;
        this.Logger = Logger;
        this.MaxRecieveBytes = 1024;
    }

    public void Start()
    {
        TcpListener Server = new TcpListener(Ip, PortNumber);
        Server.Start();
        Logger.LogInformation($"Started Server on port {Ip}:{PortNumber}");

        while(!ShouldStop)
        {
            using (Socket socket = Server.AcceptSocket())
            {
                var RequestBuff = new byte[MaxRecieveBytes];
                int RecievedCount = socket.Receive(RequestBuff);
                Logger.LogInformation($"{nameof(RecievedCount)}: {RecievedCount}");
                byte[] Response = ProcessRequest(RequestBuff);
                socket.Send(Response);
            }
        }
    }

    public void Stop()
    {
        ShouldStop = true; 
    }

    protected abstract byte[] ProcessRequest(byte[] Bytes);

}   