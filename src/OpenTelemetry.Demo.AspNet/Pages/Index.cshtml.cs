using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OpenTelemetry.Trace;

namespace OpenTelemetry.Demo.AspNet.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly Tracer _tracer;

    public IndexModel(ILogger<IndexModel> logger, TracerProvider provider)
    {
        _logger = logger;
        _tracer = provider.GetTracer(TelemetryConstants.MyAppTraceSource);
    }

    public void OnGet()
    {
        using var mySpan = _tracer.StartActiveSpan("MyOp").SetAttribute("httpTracer", HttpContext.TraceIdentifier);
        mySpan.AddEvent($"Received HTTP request from {Request.Headers.UserAgent}");
    }
}
