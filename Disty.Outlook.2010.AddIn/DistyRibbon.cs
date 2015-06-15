using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace Disty.Outlook._2010.AddIn
{
    public partial class DistyRibbon
    {
        private void DistyRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            //foreach (var item in customerQuery)
            //{
            this.cbLists.Items.Add(this.Factory.CreateRibbonDropDownItem());
                this.cbLists.Items.Last().Label = "test";
            //}
                this.cbLists.Text = this.cbLists.Items.First().Label;
        }
    }
}
