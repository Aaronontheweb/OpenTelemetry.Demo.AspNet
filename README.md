# OpenTelemetry.Demo.AspNet
A simple ASP.NET demo of OpenTelemetry tracing in .NET 6+

To run this sample...

### Start Jaeger
This will start Jaeger on all of its default ports. You can view the Jaeger Query UI on http://localhost:16686/

```
PS> docker-compose up -d
```

### Run the Demo
This will start the stock .NET 6 application on https://localhost:7244 / http://localhost:5210

```
PS> cd\src\OpenTelemetry.Demo.AspNet
PS> dotnet run
```