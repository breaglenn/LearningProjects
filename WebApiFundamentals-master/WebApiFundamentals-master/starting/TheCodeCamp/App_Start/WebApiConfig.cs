﻿using Microsoft.Web.Http.Versioning;
using Microsoft.Web.Http.Versioning.Conventions;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TheCodeCamp.Controllers;

namespace TheCodeCamp
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      // Web API configuration and services
      AutofacConfig.Register();

            config.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new Microsoft.Web.Http.ApiVersion(1, 1);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = //new UrlSegmentApiVersionReader();
                ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-Version"),
                    new QueryStringApiVersionReader("ver"));

                cfg.Conventions.Controller<TalksController>()
                .HasApiVersion(1, 0)
                .HasApiVersion(1, 1)
                .Action(mbox => mbox.Get(default(string), default(int), default(bool)))
                    .MapToApiVersion(2, 0);
            });

        // Change Case of JSON
        config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
            new CamelCasePropertyNamesContractResolver();


        // Web API routes
        config.MapHttpAttributeRoutes();

      //config.Routes.MapHttpRoute(
      //    name: "DefaultApi",
      //    routeTemplate: "api/{controller}/{id}",
      //    defaults: new { id = RouteParameter.Optional }
      //);
    }
  }
}
