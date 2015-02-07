using System;
using System.Web;
using System.Web.Http;
using log4net;

namespace Disty.UI.Web.Com.Disty.Www
{
    public class Global : HttpApplication
    {
        private void Application_Start(object sender, EventArgs e)
        {
            try
            {
                DistyConfig.Configure(GlobalConfiguration.Configuration);
            }
            catch (Exception exception)
            {
                var logger = LogManager.GetLogger(Common.Contract.Constants.Global.DefaultLogName);
                logger.Error(new Exception("Unable to start Disty application.  Check inner exception for details.",
                    exception));
                throw;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            Server.ClearError();

            try
            {
                var log = LogManager.GetLogger(Common.Contract.Constants.Global.DefaultLogName);
                if (exception.Message.Contains("File does not exist."))
                    log.Warn("Global HttpApplication Error Handler Catch.", exception);
                else
                    log.Error("Global HttpApplication Error Handler Catch.", exception);

                //RedirectToErrorController(exception);
            }
            catch (Exception)
            {
                // Swallow
            }
        }
    }
}