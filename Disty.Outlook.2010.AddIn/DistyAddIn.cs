﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using Autofac;
using System.Reflection;
using Disty.Common.IOC;

namespace Disty.Outlook._2010.AddIn
{
    public partial class DistyAddIn
    {
        private void DistyAddIn_Startup(object sender, System.EventArgs e)
        {
        }

        private void DistyAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
        {
            return new Ribbon();
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(DistyAddIn_Startup);
            this.Shutdown += new System.EventHandler(DistyAddIn_Shutdown);
        }
        
        #endregion
    }
}
