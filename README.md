# AspNetCore Extensions
A collection of reusable AspNetCore extensions.
 
## Version Information
Version information can be crusial when monitoring an application. Just like "health" it
is important to get information about a running application.

This extension adds an endpoint to your WebApp or Api. This information will return
a json object which contains the version of the application and the hash that represents
the last commit of the application. With this information you can always monitor which
version is running in each environment of your DTAP environments.

Here is an example of the output
``` Json
{"version":"6.2.1","commit":"b7a45f"}
```

## How to use
In your app register the following service:
``` C#
  services.AddVersionInfo();
```

In your app register the following mapping:
``` C#
  app.UseEndpoints(endpoints =>
  {
      endpoints.MapVersion("/version");

      endpoints.MapHealthChecks("/health", new HealthCheckOptions
      {
          Predicate = _ => true,
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
      });

      endpoints.MapControllers();
  });
```

## How to use
Once all registrations have been added, just start your application
and open the url: <baseUrl>/version

ie.: https://localhost:5001/version

This will show the version json.