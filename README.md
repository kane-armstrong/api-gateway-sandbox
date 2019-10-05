# API Gateway Sandbox - Ocelot

This repo contains the source for an experiment with Ocelot and Swagger. An unfortunate side effect of rerouting to your downstream services using Ocelot as a gateway
is that you lose Swagger documentation. One option is to aggregate your disparate swagger docs post-generation, make any necessary edits (for validity or otherwise) by
hand, then update your gateway source to ensure the Swagger is served. Nasty stuff. Of course you could just add a reroute directly to the Swagger docs, but that sucks.
You lost one of the benefits of going for a gateway in the first place (facade) and it requires either a priori knowledge of upstream paths to the downstream Swagger
documents, or manual intervention on the gateway, e.g. a web page serving as a directory. 

Question: can we somehow present Swagger documentation at the gateway level, without having to manually mess with json files, and in a seamless and relatively obvious fashion?

## Todo:

* [ ] Test this out with HTTP aggregation on the gateway

## Consolidating Swagger documents on the gateway

Turns out this is relatively straight forwardwhen using [OcelotSwagger](https://github.com/Rwing/OcelotSwagger).

If you set this up correctly, it exposes a typical Swagger page at path `/swagger`, with each registered downstream Swagger document served as a spec in the Swagger menu.

**Problem** - doesn't seem like executing requests from the swaggergen works. Also not sure how well this works when using client generation tools like NSwag

### Guide

1. Install OcelotSwagger on your gateway
2. In Startup.cs > `ConfigureServices`, add `services.Configure<OcelotSwaggerOptions>(Configuration.GetSection(nameof(OcelotSwaggerOptions)));` and `services.AddOcelotSwagger();`
3. In Startup.cs > `Configure`, add `app.UseOcelotSwagger();` before the `app.UseOcelot...` call
4. Ensure your reroutes are done, e.g. in ocelot.json
4. Configure appsettings.json to capture the OcelotSwaggerOptions setup (see below)

**Note**: you need ocelot to reroute to swagger.json files for the OcelotSwagger Swagger aggregation to work.

For the configuration of reroutes and OcelotSwagger, the important thing is that the URLs defined in the `OcelotSwaggerOptions` `SwaggerEndpoints` section must match an existing 
upstream reroute to a downstream swagger.json file.

Given the following reroutes:

````
{
  "ReRoutes": [
    {
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5201
        }
      ],
      "DownstreamPathTemplate": "/swagger/v1/swagger.json",
      "UpstreamPathTemplate": "/api/r/swagger",
      "UpstreamHttpMethod": [ "GET", "PUT", "PATCH", "DELETE", "OPTIONS" ],
      "ReRouteIsCaseSensitive": false,
      "DownstreamScheme": "http"
    },
    {
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 5202
        }
      ],
      "DownstreamPathTemplate": "/swagger/v1/swagger.json",
      "UpstreamPathTemplate": "/api/w/swagger",
      "UpstreamHttpMethod": [ "GET", "PUT", "PATCH", "DELETE", "OPTIONS" ],
      "ReRouteIsCaseSensitive": false,
      "DownstreamScheme": "http"
    }
  ]
}
````

You would need the following configuration:

````
{
  "OcelotSwaggerOptions": {
    "Cache": {
      "Enabled": true,
      "SlidingExpirationInSeconds": 60
    },
    "SwaggerEndpoints": [
      {
        "Name": "Recipes API",
        "Url": "/api/r/swagger"
      },
      {
        "Name": "Weather Forecast API",
        "Url": "/api/w/swagger"
      }
    ]
  }
}
````
