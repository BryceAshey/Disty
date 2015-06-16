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
            _listClient = new ListClient(new DistyClient<DistributionList>());

            var lists = _listClient.GetAsync().Result;
            foreach(var list in lists)
            {
                var item = this.Factory.CreateRibbonDropDownItem();
                item.Label = list.Name;
                item.Tag = list.Id.ToString();
                this.cbLists.Items.Add(item);
            }

            this.cbLists.Text = this.cbLists.Items.First().Label;
        }
    }
}
