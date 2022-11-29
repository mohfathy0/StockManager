using DataModel;
using DevExpress.Utils.Animation;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using DevExpress.XtraTab;
using StockManager.DAL;
using StockManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager.Forms
{
    public partial class frm_ApplicationSettings : frm_MasterEntry
    {
        List<BaseEdit> editors;
        bool byValidation = false;
        public frm_ApplicationSettings()
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
            btn_New.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btn_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            GetData();
        }
        public frm_ApplicationSettings(bool _byValidation = false)
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
            btn_New.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            btn_Delete.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            GetData();
            byValidation = _byValidation;
        }


        private void frm_ApplicationSettings_Load(object sender, EventArgs e)
        {
            //this.LookAndFeel.SkinName = "Bezier";
            accordionControl1.AnimationType = DevExpress.XtraBars.Navigation.AnimationType.Simple;
            accordionControl1.Appearance.Item.Hovered.FontStyleDelta = FontStyle.Bold;
            accordionControl1.Appearance.Item.Pressed.FontStyleDelta = FontStyle.Bold;
            accordionControl1.ScrollBarMode = DevExpress.XtraBars.Navigation.ScrollBarMode.Hidden;
            accordionControl1.OptionsMinimizing.AllowMinimizeMode = DevExpress.Utils.DefaultBoolean.False;
            accordionControl1.SelectedElement = accordionControl1.Elements.First();
            xtraTabControl1.Transition.AllowTransition = DevExpress.Utils.DefaultBoolean.True;
            xtraTabControl1.Transition.EasingMode = DevExpress.Data.Utils.EasingMode.EaseInOut;
            SlideFadeTransition transition = new SlideFadeTransition();
            transition.Parameters.EffectOptions = PushEffectOptions.FromBottom;
            xtraTabControl1.Transition.TransitionType = transition;
            xtraTabControl1.SelectedPageChanging += XtraTabControl1_SelectedPageChanging;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = byValidation && IsNew;
            base.OnClosing(e);
        }

        private void XtraTabControl1_SelectedPageChanging(object sender, TabPageChangingEventArgs e)
        {
            SlideFadeTransition transition = new SlideFadeTransition();
            var currentPageIndex = xtraTabControl1.TabPages.IndexOf(e.Page);
            var prevPageIndex = xtraTabControl1.TabPages.IndexOf(e.PrevPage);
            if (currentPageIndex > prevPageIndex)
                transition.Parameters.EffectOptions = PushEffectOptions.FromBottom;
            else
                transition.Parameters.EffectOptions = PushEffectOptions.FromTop;
            xtraTabControl1.Transition.TransitionType = transition;
        }

        private void AccordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            int SelectedIndex = accordionControl1.Elements.IndexOf(e.Element);
            xtraTabControl1.SelectedTabPageIndex = SelectedIndex;
        }
        public override void GetData()
        {
            editors = new List<BaseEdit>();
            GlobalSettingsTemplate settings = new GlobalSettingsTemplate();

            accordionControl1.Elements.Clear();
            xtraTabControl1.TabPages.Clear();
            accordionControl1.AllowItemSelection = true;
            var Catalog = settings.GetType().GetProperties();
            foreach (var item in Catalog)
            {
                accordionControl1.Elements.Add(new DevExpress.XtraBars.Navigation.AccordionControlElement()
                {
                    Name = item.Name,
                    Text = item.Name,
                    Style = DevExpress.XtraBars.Navigation.ElementStyle.Item,
                });

                var page = new XtraTabPage() { Name = item.Name, Text = item.Name };
                xtraTabControl1.TabPages.Add(page);
                LayoutControl lc = new LayoutControl();
                var props = item.GetValue(settings).GetType().GetProperties();
                foreach (var prop in props)
                {
                    BaseEdit edit = GlobalSettingsTemplate.GetPropertyControl(prop.Name, prop.GetValue(item.GetValue(settings)));
                    if (edit == null)
                        continue;
                    var layoutitem = lc.AddItem(prop.Name.ToSentence(), edit);
                    layoutitem.TextVisible = true;
                    layoutitem.SizeConstraintsType = SizeConstraintsType.Custom;
                    layoutitem.MinSize = new Size(400, 25);
                    layoutitem.MaxSize = new Size(400, 25);
                    if (edit.GetType() == typeof(MemoEdit))
                    {
                        layoutitem.MinSize = new Size(400, 100);
                        layoutitem.MaxSize = new Size(600, 100);
                    }
                    editors.Add(edit);
                }
                xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                lc.Dock = DockStyle.Fill;
                page.Controls.Add(lc);
            }
            base.GetData();
        }


        public override bool IsDataValid()
        {
            base.IsDataValid();
            int errorCount = 0;
            editors.ForEach(e =>
            {
                bool isValid = false;
                if (e.GetType() == typeof(LookUpEdit))
                {
                    isValid = ((LookUpEdit)e).IsEditValueNumberAndNotZero();
                    errorCount += isValid ? 0 : 1;
                    if (!isValid) ShowMenuError(e.Parent.Parent.TabIndex);
                }
                else if (e.GetType() == typeof(SpinEdit))
                {
                    isValid = ((SpinEdit)e).IsValueGreaterThanZero();
                    errorCount += isValid ? 0 : 1;
                    if (!isValid) ShowMenuError(e.Parent.Parent.TabIndex);
                }
                else if (e.GetType() == typeof(TextEdit))
                {
                    isValid = (((TextEdit)e).IsTextValid());
                    errorCount += isValid ? 0 : 1;
                    if (!isValid) ShowMenuError(e.Parent.Parent.TabIndex);
                }
                else if (e.GetType() == typeof(MemoEdit))
                {
                    isValid = (((MemoEdit)e).IsTextValid());
                    errorCount += isValid ? 0 : 1;
                    if (!isValid) ShowMenuError(e.Parent.Parent.TabIndex);
                }
            });
            accordionControl1.Refresh();
            return errorCount == 0;
        }
        private void ShowMenuError(int indexnumber)
        {
            accordionControl1.Elements[indexnumber].Text = "* " + accordionControl1.Elements[indexnumber].Text.Replace("* ", "").Replace(" !", "") + " !";
        }

        public override void Save()
        {
            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                editors.ForEach(e =>
                {
                    var setvalue = e.GetType() == typeof(SpinEdit) ? e.EditValue.ToInt() : e.EditValue;
                    ctx.GlobalSettings.AddOrUpdate(new GlobalSetting()
                    {
                        Id = e.Tag.ToInt(),
                        SettingName = e.Name,

                        SettingValue = DataMaster.ToByteArray<object>(setvalue)
                    });
                });
                ctx.SaveChanges();
                frm_MainForm.Instance.EnableDisableAccordionControl(true);
            }
            Session.RefreshGlobalSettings();
            CloseWaitForm();

            base.Save();
        }
    }
}