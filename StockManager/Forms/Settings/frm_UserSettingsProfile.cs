using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System.Data.Entity.Migrations;
using DevExpress.Utils.Animation;
using DataModel;
using StockManager.DAL;
using StockManager.Utilities;

namespace StockManager.Forms
{
    public partial class frm_UserSettingsProfile : frm_MasterEntry
    {
        UserSettingsProfile profile;
        List<BaseEdit> editors;
        public frm_UserSettingsProfile()
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
            New();

        }
        public frm_UserSettingsProfile(int Id)
        {
            InitializeComponent();
            accordionControl1.ElementClick += AccordionControl1_ElementClick;
            using (DatabaseContext ctx = new DatabaseContext())
            {
                ShowWaitForm();
                profile = ctx.UserSettingsProfiles.Single(x => x.Id == Id);
                CloseWaitForm();
                GetData();
            }
        }
        private void frm_UserSettingsProfile_Load(object sender, EventArgs e)
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

        public override void New()
        {
            profile = new UserSettingsProfile();
            base.New();
        }
        public override void GetData()
        {
            editors = new List<BaseEdit>();
            txt_Name.Text = profile.Name;
            UserSettingsTemplate settings = new UserSettingsTemplate(profile.Id);
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

                    BaseEdit edit = UserSettingsTemplate.GetPropertyControl(prop.Name, prop.GetValue(item.GetValue(settings)));
                    if (edit == null)
                        continue;
                    var layoutitem = lc.AddItem(prop.Name.ToSentence(), edit);
                    layoutitem.TextVisible = true;
                    layoutitem.SizeConstraintsType = SizeConstraintsType.Custom;
                    layoutitem.MaxSize = new Size(400, 25);
                    layoutitem.MinSize = new Size(250, 25);
                    editors.Add(edit);
                }
                xtraTabControl1.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

                lc.Dock = DockStyle.Fill;
                page.Controls.Add(lc);
            }
            base.GetData();
        }
        private void AccordionControl1_ElementClick(object sender, DevExpress.XtraBars.Navigation.ElementClickEventArgs e)
        {
            int SelectedIndex = accordionControl1.Elements.IndexOf(e.Element);
            xtraTabControl1.SelectedTabPageIndex = SelectedIndex;
        }
        public override bool IsDataValid()
        {
            base.IsDataValid();

            int errorCount = 0;
            errorCount += txt_Name.IsTextValid() ? 0 : 1;
            editors.ForEach(e =>
            {
                if (e.GetType() == typeof(LookUpEdit))
                    errorCount += (((LookUpEdit)e).IsEditValueNumberAndNotZero()) ? 0 : 1;
            });
            return errorCount == 0;
        }
        public override void Save()
        {
            this.Text = profile.Name = txt_Name.Text;

            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                ctx.UserSettingsProfiles.AddOrUpdate(profile);
                ctx.SaveChanges();
                ctx.UserSettingsProfileProperties.RemoveRange(ctx.UserSettingsProfileProperties.Where(x => x.ProfileId == profile.Id));
                editors.ForEach(e =>
                {
                    var value = e.EditValue;
                    if (e.Name == nameof(UserSettingsTemplate.General.MinimumBackwardSearchInDays))
                    {
                        value = e.EditValue.ToInt();
                    }
                    ctx.UserSettingsProfileProperties.Add(new UserSettingsProfileProperty()
                    {
                        ProfileId = profile.Id,
                        PropertyName = e.Name,
                        PropertyValue = DataMaster.ToByteArray<object>(value)
                    });
                });
                ctx.SaveChanges();
            }
            CloseWaitForm();
            this.Text = recordName = profile.Name;
            recordId = profile.Id;
            base.Save();
        }
        public override void Delete()
        {
            throw new NotImplementedException();
            base.Delete();
        }
    }
}