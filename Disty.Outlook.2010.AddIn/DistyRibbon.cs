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
    public partial class DistyRibbon
    {
        private IDistributionListService _listClient;
        

        private void DistyRibbon_Load(object sender, RibbonUIEventArgs e)
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
                this.cbLists.Items.Add(defaultItem);

                foreach (var list in lists)
                {
                    var item = this.Factory.CreateRibbonDropDownItem();
                    item.Label = list.Name;
                    item.Tag = list.Id.ToString();
                    this.cbLists.Items.Add(item);
                }

                this.cbLists.Text = this.cbLists.Items.First().Label;
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
                _listClient = new ListClient(new DistyClient<DistributionList>());
                var listName = ((Microsoft.Office.Tools.Ribbon.RibbonComboBox)sender).Text as string;
                var item = ((Microsoft.Office.Tools.Ribbon.RibbonComboBox)sender).Items.Where(x => x.Label == listName).FirstOrDefault();
                if (item == null)
                    return;

                int listId;
                if (!int.TryParse(item.Tag as string, out listId))
                    return;

                dynamic context = e.Control.Context.CurrentItem;
                if(listId == 0)
                {
                    context.To = "";
                    return;
                }

                var list = _listClient.GetAsync(listId).Result;
                if (list == null || !list.Emails.Any())
                    return;
                
                context.To = string.Join("; ", list.Emails.Select(a => a.Address));
            }
            catch (Exception ex)
            {
                var exception = ex;
            }

        }
    }
}
