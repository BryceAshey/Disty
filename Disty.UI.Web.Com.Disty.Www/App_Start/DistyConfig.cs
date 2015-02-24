using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using Disty.Common.Contract.Constants;
using Disty.Common.IOC;
using Disty.Common.Log.Exceptions;
using Disty.Common.Net.Http.Formatting;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Disty.UI.Web.Com.Disty.Www
{
    public static class DistyConfig
    {
        public static Autofac.IContainer Configure(HttpConfiguration httpConfiguration)
        {
            try
            {
                // IOC initiation happens here
                var container = ContainerInstanceProvider.GetContainerInstance(Assembly.GetCallingAssembly());

                // Configure the site
                Configure(httpConfiguration, container);
                httpConfiguration.EnsureInitialized();

                // Return the contianer
                return container;
            }
            catch (LoggedException)
            {
                throw;
            }
            catch (Exception exception)
            {
                // Attempt to log it
                var log = LogManager.GetLogger(Common.Contract.Constants.Global.DefaultLogName);
                log.Error(exception);

                throw exception;
            }
        }

        private static void Configure(HttpConfiguration httpConfiguration, Autofac.IContainer container)
        {
            try
            {
                ConfigureLogging();
                ConfigureMapping();
                //ConfigureEnterpriseLibraries();

                ConfigureWebApi(httpConfiguration, container);
                ConfigureSite(httpConfiguration, container);
            }
            catch (Exception exception)
            {
                // Attempt to log it
                var log = LogManager.GetLogger(Common.Contract.Constants.Global.DefaultLogName);
                log.Error(exception);

                throw new LoggedException("Error in DistyConfiguration startup.", exception);
            }
        }

        #region Logging

        private static void ConfigureLogging()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(HttpRuntime.AppDomainAppPath + "log4net.config"));
        }

        #endregion

        #region Mapping

        private static void ConfigureMapping()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.AddProfile(new PlatformMapProfile());
            });
        }

        #endregion

        #region Enterprise Libraries

        //private static void ConfigureEnterpriseLibraries()
        //{
        //    using (var config = new SystemConfigurationSource())
        //    {
        //        var settings = RetryPolicyConfigurationSettings.GetRetryPolicySettings(config);

        //        // Initialize the RetryPolicyFactory with a RetryManager built from the 
        //        // settings in the configuration file.
        //        RetryPolicyFactory.SetRetryManager(settings.BuildRetryManager());

        //    }
        //}

        #endregion

        #region Site

        private static void ConfigureSite(HttpConfiguration httpConfiguration, Autofac.IContainer container)
        {
            ConfigureSiteDependencyResolver(httpConfiguration, container);

            AreaRegistration.RegisterAllAreas();
            ConfigureSiteGlobalFilters(GlobalFilters.Filters);
            ConfigureSiteRouting();
        }

        private static void ConfigureSiteGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new System.Web.Mvc.AuthorizeAttribute());
        }

        private static void ConfigureSiteDependencyResolver(HttpConfiguration httpConfiguration, Autofac.IContainer container)
        {
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void ConfigureSiteRouting()
        {
            var routes = RouteTable.Routes;

            routes.RouteExistingFiles = false;
            routes.IgnoreRoute("{*staticfile}", new { staticfile = @".*\.(css|php|js|gif|jpg|png|ico)(/.*)?" });  //css|js|gif|jpg|png
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Disty.UI.Web.Com.Disty.Www.Controllers" }
            );
        }

        #endregion

        #region WebApi

        private static void ConfigureWebApi(HttpConfiguration httpConfiguration, Autofac.IContainer container)
        {
            ConfigureWebApiDependencyResolver(httpConfiguration, container);
            ConfigureWebApiRouting(httpConfiguration);
            ConfigureWebApiSerialization(httpConfiguration);
        }

        public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
        {
            filters.Add(new System.Web.Http.AuthorizeAttribute());
        }

        private static void ConfigureWebApiDependencyResolver(HttpConfiguration httpConfiguration, Autofac.IContainer container)
        {
            httpConfiguration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void ConfigureWebApiRouting(HttpConfiguration httpConfiguration)
        {
            httpConfiguration.MapHttpAttributeRoutes();
        }

        private static void ConfigureWebApiSerialization(HttpConfiguration httpConfiguration)
        {
            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.Converters.Add(new IsoDateTimeConverter());
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            serializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            serializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            serializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
            httpConfiguration.Formatters[0] = new JsonNetFormatter(serializerSettings);
        }



        #endregion

    }
}
