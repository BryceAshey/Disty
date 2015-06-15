using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Office.Interop.Outlook;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

namespace Disty.Outlook._2013.AddIn
{
    public partial class Disty
    {
        Inspectors inspectors;

        private void Disty_Startup(object sender, System.EventArgs e)
        {
            inspectors = this.Application.Inspectors;
            inspectors.NewInspector +=
                new InspectorsEvents_NewInspectorEventHandler(Target);
        }

        private void Target(Inspector inspector)
        {
            var item = inspector.CurrentItem;
        }

        private void Disty_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(Disty_Startup);
            this.Shutdown += new System.EventHandler(Disty_Shutdown);
        }
        
        #endregion
    }
}
