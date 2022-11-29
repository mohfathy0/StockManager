using DataModel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using StockManager.Forms;
using StockManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace StockManager
{
  
    public partial class frm_MainForm : DevExpress.XtraBars.TabForm
    {
        private static frm_MainForm _instance;
        private TabFormContentContainer tabFormContentContainer;
        private TabFormPage tabFormPage;
        private FormWindowState state;
        private static readonly log4net.ILog log = Utilities.LogHelper.GetLogger();
        public static frm_MainForm Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frm_MainForm();
                }
                return _instance;
            }
        }
        private void FatalExceptionObject(Exception e)
        {
            Logger.LogException(e.TargetSite.Name, Master.GetClassName(e), Master.GetLineNumber(e), e.Message, Session.User.Username, Session.User.Id, "N/a", 0, frm_Master.ActionTypeException.NA.ToString(), "N/A", e);

        }
        public frm_MainForm()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => FatalExceptionObject(e: (Exception)e.ExceptionObject);
            InitializeComponent();
            _instance = this;
            //TODO End
            tabFormControl.SelectedPageChanged += TabFormControl_SelectedPageChanged;
            accordionControl.ElementClick += AccordionControl1_ElementClick;
            this.FormClosing += Frm_MainForm_FormClosing;
            this.Load += Frm_MainForm_Load;
        }
        private void Frm_MainForm_Load(object sender, EventArgs e)
        {
            tabFormControl.OuterFormCreated += TabFormControl_OuterFormCreated; ;
        }
        private void TabFormControl_OuterFormCreated(object sender, OuterFormCreatedEventArgs e)
        {
            e.Form.Width = 800;
            e.Form.Height = 600;
            e.Form.TabFormControl.ShowAddPageButton = false;
            e.Form.TabFormControl.ShowTabsInTitleBar = ShowTabsInTitleBar.True;
        }
        private void TabFormControl_SelectedPageChanged(object sender, TabFormSelectedPageChangedEventArgs e)
        {
            if (e.Page == null) return;
            e.Page.ContentContainer.Visible = false;
            BeginInvoke(new Action(() =>
            {
                e.Page.ContentContainer.SuspendLayout();
                e.Page.ContentContainer.BringToFront();
                e.Page.ContentContainer.ResumeLayout();
                e.Page.ContentContainer.Visible = true;
            }));
        }
        public void InitializeAccordionControl()
        {
            accordionControl.Elements.Clear();
              var screens = Session.ScreenAccess.Where(x => (x.CanShow == true || (Session.User.UserType == Master.UserType.Admin.ToByte()) && x.Actions.Contains(Master.Actions.Show)));
         //   var screens = Screens.GetScreens;
            screens.Where(s => s.ParentScreenId == 0).ToList().ForEach(s =>
            {
                AccordionControlElement elm = new AccordionControlElement()
                {
                    Text = s.ScreenCaption,
                    Tag = s.ScreenName,
                    Name = s.ScreenName,
                    Style = ElementStyle.Group
                };
                accordionControl.Elements.Add(elm);
                AddAccordionElement(elm, s.ScreenId);

            });
            accordionControl.ExpandAll();
        }
        void AddAccordionElement(AccordionControlElement parent, int parentId)
        {
            var screens = Session.ScreenAccess.Where(x => (x.CanShow == true || (Session.User.UserType == Master.UserType.Admin.ToByte()) && x.Actions.Contains(Master.Actions.Show)));
           // var screens = Screens.GetScreens;
            screens.Where(s => s.ParentScreenId == parentId).ToList().ForEach(s =>
            {
                AccordionControlElement elm = new AccordionControlElement()
                {
                    Text = s.ScreenCaption,
                    Tag = s.ScreenName,
                    Name = s.ScreenName,
                    Style = ElementStyle.Item
                };
                parent.Elements.Add(elm);
            });
        }
        public void EnableDisableAccordionControl(bool enable)
        {
              accordionControl.Enabled = enable;
        }
        private void AccordionControl1_ElementClick(object sender, ElementClickEventArgs e)
        {
            accordionControl.ClosePopupForm();

            var tag = e.Element.Tag as string;
            if (tag != string.Empty)
            {
                // TODO uncomment and add methodOpenFormInTab
                OpenFormByName(tag);
            }
        }
        private void Frm_MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void OpenFormByName(string tag)
        {
            Form form;
            switch (tag)
            {

                default:
                    var ins = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.Name == tag);
                    if (ins != null)
                    {
                        form = Activator.CreateInstance(ins) as Form;
                        form.Name = tag;
                        form.Text = Screens.GetScreens.Where(x => x.ScreenName == form.Name).FirstOrDefault()?.ScreenCaption;
                        OpenFormWithPermission(form);
                    }
                    break;
            }
        }
        public void OpenFormWithPermission(Form frm)
        {
            var screen = Session.ScreenAccess.SingleOrDefault(x => x.ScreenName == frm.Name);
            if (Session.User.UserType == (byte)Master.UserType.Admin)
            {
                OpenFormInTab(frm);
                return;
            }
            if (screen != null)
            {
                if (screen.CanOpen)
                {
                    OpenFormInTab(frm);
                    return;
                }
                XtraMessageBox.Show("Your access level does not permit this operation!", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState != FormWindowState.Minimized)
                state = this.WindowState;
            base.OnResize(e);
        }
        private void TabFormControl_PageClosing(object sender, PageClosingEventArgs e)
        {
            Form frm = Application.OpenForms.Cast<Form>().Where(f => f.Name == e.Page.Name && object.Equals(f.Tag, e.Page.Tag)).FirstOrDefault();
            if (frm != null)
                frm.Close();
            frm = Application.OpenForms.Cast<Form>().Where(f => f.Name == e.Page.Name && object.Equals(f.Tag, e.Page.Tag)).FirstOrDefault();
            if (frm != null)
            {
                e.Cancel = true;
                return;
            }
        }
        public void OpenFormInTab(Form form)
        {

            if (Application.OpenForms[form.Name] != null)
            {
                var page = TabFormControl.Pages.Where(x => x.Name == form.Name && object.Equals(x.Tag, form.Tag)).FirstOrDefault();
                if (page != null)
                {
                    TabFormControl.SelectedPage = page;
                    if (Instance.WindowState == FormWindowState.Minimized)
                        this.WindowState = state;
                    if (!Instance.Focused)
                    {
                        Instance.Activate();
                        Instance.Focus();
                    }
                    return;
                }
            }
            form.Dock = DockStyle.Fill;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            this.tabFormContentContainer = new DevExpress.XtraBars.TabFormContentContainer();
            this.tabFormPage = new DevExpress.XtraBars.TabFormPage();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl)).BeginInit();
            this.SuspendLayout();
            this.tabFormControl.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl.Name = "tabFormControl";
            this.tabFormControl.Pages.Add(this.tabFormPage);
            this.tabFormControl.SelectedPage = this.tabFormPage;
            this.tabFormControl.Size = new System.Drawing.Size(284, 50);
            this.tabFormControl.MaxTabWidth = 0;
            this.tabFormControl.TabForm = this;
            this.tabFormControl.TabIndex = 0;
            this.tabFormControl.TabStop = false;
            this.tabFormControl.PageClosing += TabFormControl_PageClosing;
            this.tabFormContentContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer.Location = new System.Drawing.Point(0, 50);
            this.tabFormContentContainer.Name = "tabFormContentContainer";
            this.tabFormContentContainer.Size = new System.Drawing.Size(284, 211);
            this.tabFormContentContainer.TabIndex = 1;
            this.tabFormPage.ContentContainer = this.tabFormContentContainer;
            this.tabFormPage.Name = form.Name;
            this.tabFormPage.Text = form.Text;
            this.tabFormPage.Tag = form.Tag;
            this.tabFormContentContainer.Controls.Add(form);

            try
            {
                form.Show();
            }
            catch (Exception e)
            {
                ExceptionLog ex = new ExceptionLog();
                frm_Master f = new frm_Master();
                ex.ActionType = frm_Master.ActionTypeException.NA.ToString();
                ex.ScreenName = this.Name;
                if ((form.GetType().BaseType).BaseType == typeof(frm_Master))
                {
                    f = ((frm_Master)form);
                    ex.ScreenName = f.Name;
                    ex.RecordId = f.recordId;
                    ex.RecordName = f.recordName;
                    var action = f.currentAction;
                    ex.ActionType = action.ToString();
                }
                ex.UserId = Session.User.Id;
                ex.UserName = Session.User.Username;
                ex.LineNumber = Master.GetLineNumber(e);
                ex.ClassName = Master.GetClassName(e);
                ex.MethodName = e.TargetSite.Name;
                ex.logMessage = e.Message;
                Logger.LogException(ex.MethodName, ex.ClassName, ex.LineNumber, ex.logMessage, ex.UserName, ex.UserId, ex.RecordName, ex.RecordId, ex.ActionType, ex.ScreenName, e);
                f.CloseWaitForm();
                frm_ExceptionMessage exDialog = new frm_ExceptionMessage(ex.logMessage, e.StackTrace, f);
                exDialog.ShowDialog();

            }
            form.Activate();
            form.Focus();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl)).EndInit();
            this.ResumeLayout(true);
        }

        public void RenameTab(Form form)
        {
            if (Application.OpenForms[form.Name] != null)
            {
                var page = TabFormControl.Pages.Where(x => x.Name == form.Name && object.Equals(x.Tag, form.Tag)).FirstOrDefault();
                if (page != null)
                {
                    page.Text = page.Name = form.Text;
                    page.Tag = form.Tag;
                }
            }
        }
        public void CloseTab(Form form)
        {
            if (Application.OpenForms[form.Name] != null)
            {
                var page = TabFormControl.Pages.Where(x => x.Name == form.Name && object.Equals(x.Tag, form.Tag)).FirstOrDefault();
                if (page != null)
                {
                    TabFormControl.ClosePage(page);
                }
                if (form != null)
                    form.Close();
            }
        }
        public void CloseCurrentTab()
        {
            var pageInfos = tabFormControl.ViewInfo.PageInfos.Where(x => x.Page.Name == tabFormControl.SelectedPage.Name && x.Page.Tag == tabFormControl.SelectedPage.Tag).FirstOrDefault();
            if (pageInfos != null)
            {
                var page = pageInfos.Page;

                TabFormControl.ClosePage(page);
            }
        }
    }
}
