using System.Diagnostics;
using OpenTelemetry.Trace;

namespace OpenTelemetry.Demo.AspNet;

public static class TelemetryConstants
{
    /// <summary>
    /// The name of the <see cref="ActivitySource"/> that is going to produce our traces.
    /// </summary>
    public const string MyAppTraceSource = "Demo.AspNet";
}