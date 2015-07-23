namespace Disty.Outlook._2010.AddIn
{
    partial class DistyRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public DistyRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.cbLists = this.Factory.CreateRibbonComboBox();
            this.butManage = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabNewMailMessage";
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabNewMailMessage";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this.cbLists);
            this.group1.Items.Add(this.butManage);
            this.group1.Label = "Distribution List";
            this.group1.Name = "group1";
            this.group1.Position = this.Factory.RibbonPosition.BeforeOfficeId("GroupClipboard");
            // 
            // cbLists
            // 
            this.cbLists.Label = "List";
            this.cbLists.Name = "cbLists";
            this.cbLists.Text = null;
            this.cbLists.TextChanged += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cbLists_TextChanged);
            // 
            // butManage
            // 
            this.butManage.Label = "Manage Lists";
            this.butManage.Name = "butManage";
            this.butManage.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.butManage_Click);
            // 
            // DistyRibbon
            // 
            this.Name = "DistyRibbon";
            this.RibbonType = "Microsoft.Outlook.Appointment, Microsoft.Outlook.Mail.Compose, Microsoft.Outlook." +
    "Response.Compose";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.DistyRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonComboBox cbLists;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton butManage;
    }

    partial class ThisRibbonCollection
    {
        internal DistyRibbon DistyRibbon
        {
            get { return this.GetRibbon<DistyRibbon>(); }
        }
    }
}
