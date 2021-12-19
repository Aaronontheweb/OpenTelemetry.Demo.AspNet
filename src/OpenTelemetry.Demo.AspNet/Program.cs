using OpenTelemetry.Demo.AspNet;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddOpenTelemetryTracing(b =>
{
    // uses the default Jaeger settings
    b.AddJaegerExporter();
    
    // receive traces from our own custom sources
    b.AddSource(TelemetryConstants.MyAppTraceSource);
    
    // decorate our service name so we can find it when we look inside Jaeger
    b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("AspNet", "Demo"));
    
    // receive traces from built-in sources
    b.AddHttpClientInstrumentation();
    b.AddAspNetCoreInstrumentation();
    b.AddSqlClientInstrumentation();
    
    
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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
