using System.Diagnostics;

internal abstract class HttpMessage
{
    public string RawMessageString { get; }
    public string StartLine { get; set; }
    public string? Body { get; set; }
    protected string CRLF = "\r\n";

    public Dictionary<string, string> Headers { get; protected set; } = new Dictionary<string, string>();

    public HttpMessage(string RawMessageString)
    {
        if (string.IsNullOrWhiteSpace(RawMessageString))
        {
            throw new ArgumentException($"'{nameof(RawMessageString)}' cannot be a null or whitespace", nameof(RawMessageString));
        }
        this.RawMessageString = RawMessageString;

        var MessageLines  = RawMessageString.Split(CRLF);
        Debug.Assert(MessageLines.Length > 0, "Http  message should have at least a Start Line");

        StartLine = MessageLines[0];
        ProcessStartLine(StartLine);

        foreach (var L in MessageLines.Skip(1))
        {
            if (L.Contains(":"))
            {
                var SplittedLine = L.Split(":");
                Headers.Add(SplittedLine[0], SplittedLine[1]);
            }
        }

        // ProcessHeaders(MessageLines.TakeWhile(x => x != $"{CRLF}{CRLF}"));
    }

    protected void ProcessHeaders(IEnumerable<string> Headers)
    {
        this.Headers = Headers.Select(x => x.Split(':')).ToDictionary(h => h[0], h => h[1]);
    }

    public HttpMessage()
    {
        
    }

    public abstract void ProcessStartLine(string startLine);

    public override string ToString()
    {
        return $"{StartLine}{CRLF}" +
               $"{string.Join(CRLF, Headers.Select(kvp=> $"{kvp.Key}:{kvp.Value}"))}" +
               $"{CRLF}" +
               $"{Body}";
    }

}