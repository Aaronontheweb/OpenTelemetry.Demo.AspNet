using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenTelemetry.Trace;
using static OpenTelemetry.Demo.AspNet.TelemetryConstants;

namespace OpenTelemetry.Demo.AspNet.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Tracer _tracer;

    public IndexModel(ILogger<IndexModel> logger, TracerProvider provider)
    {
        _logger = logger;
        _tracer = provider.GetTracer(TelemetryConstants.MyAppSource);
    }

    public void OnGet()
    {
        var tags = new TagList();
        tags.Add("user-agent", Request.Headers.UserAgent);
        HitsCounter.Add(1, tags);
        using var mySpan = _tracer.StartActiveSpan("MyOp").SetAttribute("httpTracer", HttpContext.TraceIdentifier);
        mySpan.AddEvent($"Received HTTP request from {Request.Headers.UserAgent}");
    }
}
