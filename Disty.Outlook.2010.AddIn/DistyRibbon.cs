using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Disty.Common.Contract.Distributions;
using Disty.Service.Client;
using Disty.Service.Interfaces;
using Microsoft.Office.Tools.Ribbon;

namespace Disty.Outlook._2010.AddIn
{
    /// <summary>
    /// Good Reference Article:  https://www.add-in-express.com/creating-addins-blog/2015/01/16/outlook-calendar-appointments-meeting-requests/
    /// </summary>
    public partial class DistyRibbon
    {
        private IDistributionListService _listClient;
        

        private void DistyRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            try
            {
                
                LoadList(this.cbLists);
                LoadList(this.cbCalendarLists);

            }
            catch(Exception ex)
            {
                var exception = ex;
            }
        }


        private void cbLists_TextChanged(object sender, RibbonControlEventArgs e)
        {   
            try
            {
                var cb = (Microsoft.Office.Tools.Ribbon.RibbonComboBox)sender;

                _listClient = new ListClient(new DistyClient<DistributionList>());
                var listName = cb.Text as string;
                var item = cb.Items.Where(x => x.Label == listName).FirstOrDefault();
                if (item == null)
                    return;

                int listId;
                if (!int.TryParse(item.Tag as string, out listId))
                    return;

                dynamic context = e.Control.Context.CurrentItem;
                if (listId == 0)
                {
                    context.To = "";
                    return;
                }

                var list = _listClient.GetAsync(listId).Result;
                if (list == null || !list.Emails.Any())
                    return;

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

        private void cbCalendarLists_TextChanged(object sender, RibbonControlEventArgs e)
        {
            try
            {
                var cb = (Microsoft.Office.Tools.Ribbon.RibbonComboBox)sender;

                _listClient = new ListClient(new DistyClient<DistributionList>());
                var listName = cb.Text as string;
                var item = cb.Items.Where(x => x.Label == listName).FirstOrDefault();
                if (item == null)
                    return;

                int listId;
                if (!int.TryParse(item.Tag as string, out listId))
                    return;

                dynamic context = e.Control.Context.CurrentItem;
                if (listId == 0)
                {
                    context.To = "";
                    return;
                }

                var list = _listClient.GetAsync(listId).Result;
                if (list == null || !list.Emails.Any())
                    return;
                
                var recipients = context.Recipients;
                foreach (var email in list.Emails)
                {
                    var recipient = recipients.Add(email.Address);
                    recipient.Type = (int)Microsoft.Office.Interop.Outlook.OlMeetingRecipientType.olRequired;
                    recipient.Resolve();
                }                
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        private void butManage_Click(object sender, RibbonControlEventArgs e)
        {
            OpenManage();
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            OpenManage();
        }

        private void LoadList(RibbonComboBox cb)
        {
            try
            {
                _listClient = new ListClient(new DistyClient<DistributionList>());

                var lists = _listClient.GetAsync().Result;
                if (lists == null)
                    return;

                var defaultItem = this.Factory.CreateRibbonDropDownItem();
                defaultItem.Label = " ";
                defaultItem.Tag = "0";
                cb.Items.Add(defaultItem);

                foreach (var list in lists)
                {
                    var item = this.Factory.CreateRibbonDropDownItem();
                    item.Label = list.Name;
                    item.Tag = list.Id.ToString();
                    cb.Items.Add(item);
                }

                cb.Text = cb.Items.First().Label;
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
    }
}
