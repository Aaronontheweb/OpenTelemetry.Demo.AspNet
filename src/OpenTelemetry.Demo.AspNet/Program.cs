using OpenTelemetry.Demo.AspNet;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// shared Resource to use for both OTel metrics AND tracing
var resource = ResourceBuilder.CreateDefault().AddService("AspNet", "Demo");

builder.Services.AddOpenTelemetryTracing(b =>
{
    // uses the default Jaeger settings
    b.AddJaegerExporter();
    
    // receive traces from our own custom sources
    b.AddSource(TelemetryConstants.MyAppSource);
    
    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(resource);
    
    // receive traces from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
    b.AddSqlClientInstrumentation();
});

builder.Services.AddOpenTelemetryMetrics(b =>
{
    // add prometheus exporter
    b.AddPrometheusExporter();
    
    // receive metrics from our own custom sources
    b.AddMeter(TelemetryConstants.MyAppSource);
    
    // decorate our service name so we can find it when we look inside Prometheus
    b.SetResourceBuilder(resource);
    
    // receive metrics from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

// add the /metrics endpoint which will be scraped by Prometheus
app.UseOpenTelemetryPrometheusScrapingEndpoint();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
