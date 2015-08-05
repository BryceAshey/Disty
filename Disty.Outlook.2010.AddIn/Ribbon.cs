using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Ribbon;
using Disty.Service.Interfaces;
using Disty.Service.Client;
using Disty.Common.Contract.Distributions;

// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.

/// Good Reference:  https://msdn.microsoft.com/en-us/library/vstudio/aa722523.aspx
/// Also:  https://msdn.microsoft.com/en-us/library/Dd548011(v=office.12).aspx

namespace Disty.Outlook._2010.AddIn
{
    [ComVisible(true)]
    public class Ribbon : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;
        private IDistributionListService _listClient;
        private IEnumerable<DistributionList> _list;

        public Ribbon()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            LoadList();

            string resourceText = String.Empty;
            switch (ribbonID)
            {
                case "Microsoft.Outlook.Mail.Compose":
                case "Microsoft.Outlook.Response.Compose":
                    resourceText = "Disty.Outlook._2010.AddIn.NewMailMessageRibbon.xml";
                    break;
                case "Microsoft.Outlook.Appointment":
                    resourceText = "Disty.Outlook._2010.AddIn.AppointmentRibbon.xml";
                    break;
            }
            return GetResourceText(resourceText);
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        public int OnGetItemCount(Office.IRibbonControl control)
        {
            return _list.Count<DistributionList>();
        }

        public string OnGetItemLabel(Office.IRibbonControl control, int index)
        {
            var label = "";
            var list = _list.ElementAt<DistributionList>(index);
            if(list != null)
                label = _list.ElementAt<DistributionList>(index).Name;

            return label;
        }

        public void cbLists_TextChanged(Office.IRibbonControl control, string controlId, int selectedIndex)
        {
            try
            {
                _listClient = new ListClient(new DistyClient<DistributionList>());
                var item = _list.ElementAt<DistributionList>(selectedIndex);
                if (item == null)
                    return;
                                
                var list = _listClient.GetAsync(item.Id).Result;
                if (list == null || !list.Emails.Any())
                    return;

                dynamic context = control.Context.CurrentItem;
                var recipients = context.Recipients;
                
                foreach (var email in list.Emails)
                {
                    var recipient = recipients.Add(email.Address);
                    recipient.Resolve();
                }
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }
        
        public void cbCalendarLists_TextChanged(Office.IRibbonControl control, string controlId, int selectedIndex)
        {
            try
            {
                _listClient = new ListClient(new DistyClient<DistributionList>());
                var item = _list.ElementAt<DistributionList>(selectedIndex);
                if (item == null)
                    return;
                
                var list = _listClient.GetAsync(item.Id).Result;
                if (list == null || !list.Emails.Any())
                    return;


                dynamic context = control.Context.CurrentItem;                
                var recipients = context.Recipients;

                foreach (var email in list.Emails)
                {
                    var recipient = recipients.Add(email.Address);
                    recipient.Type = (int)Microsoft.Office.Interop.Outlook.OlMeetingRecipientType.olRequired;
                    recipient.Resolve();
                }
                context.MeetingStatus = Microsoft.Office.Interop.Outlook.OlMeetingStatus.olMeeting;
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        public void butManage_Click(Office.IRibbonControl control)
        {
            OpenManage();
        }

        public void button1_Click(Office.IRibbonControl control)
        {
            OpenManage();
        }

        private void LoadList()
        {
            try
            {
                _listClient = new ListClient(new DistyClient<DistributionList>());

                _list = _listClient.GetAsync().Result;
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        private void OpenManage()
        {
            try
            {
                System.Diagnostics.Process.Start(Disty.Service.Client.DistyClient<Disty.Common.Contract.Distributions.DistributionDept>.DistyUrl());
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
