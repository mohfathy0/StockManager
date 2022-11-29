using DataModel;
using DevExpress.XtraEditors;
using Liphsoft.Crypto.Argon2;
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
    public partial class frm_User : frm_MasterEntry
    {
        User user;
        public frm_User()
        {
            InitializeComponent();
            this.Resize += Frm_User_Resize;
            New();


        }
        public frm_User(int Id)
        {
            InitializeComponent();
            this.Resize += Frm_User_Resize;
            using (DatabaseContext ctx = new DatabaseContext())
            {
                user = ctx.Users.SingleOrDefault(x => x.Id == Id);
                GetData();
            }

        }
        private void Frm_User_Resize(object sender, EventArgs e)
        {
            ResizeEmptySpace(emptySpaceItemRight, emptySpaceItemLeft, emptySpaceItemTop);

        }

        private void Frm_User_Load(object sender, EventArgs e)
        {
            RefreshData();
            toggleSwitch1.Properties.OffText = "Active";
            toggleSwitch1.Properties.OnText = "Inactive";
            txt_Password.Enter += Txt_Password_Enter;

        }

        private void Txt_Password_Enter(object sender, EventArgs e)
        {
            var edit = ((DevExpress.XtraEditors.TextEdit)sender);
            BeginInvoke(new MethodInvoker(() =>
            {
                edit.SelectionStart = 0;
                edit.SelectionLength = edit.Text.Length;
            }));
        }





        public override void New()
        {
            user = new User();
            base.New();
        }
        public override void RefreshData()
        {
            ShowWaitForm();

            using (DatabaseContext ctx = new DatabaseContext())
            {

                lkp_AccessProfile.InitializeData(ctx.UserAccessProfiles.ToList());
                lkp_SettingProfile.InitializeData(ctx.UserSettingsProfiles.ToList());
                lkp_UserType.InitializeData(Master.UserTypes);
            }
            CloseWaitForm();
            base.RefreshData();
        }
        public override void GetData()
        {
            txt_Username.Text = user.Username;
            toggleSwitch1.IsOn = user.Inactive;
            txt_Password.Text = user.Password;
            lkp_UserType.EditValue = user.UserType;
            lkp_SettingProfile.EditValue = user.SettingsProfileId;
            lkp_AccessProfile.EditValue = user.ScreenProfileId;
            base.GetData();
        }
        public override void SetData()
        {
            this.Text = user.Username = txt_Username.Text.Trim();
            if (user.Password != txt_Password.Text)
                user.Password = txt_Password.Text = new PasswordHasher().Hash(txt_Password.Text);
            user.Inactive = toggleSwitch1.IsOn;
            user.UserType = lkp_UserType.EditValue.ToByte();
            user.SettingsProfileId = lkp_SettingProfile.EditValue.ToInt();
            user.ScreenProfileId = lkp_AccessProfile.EditValue.ToInt();
            user.ModifiedBy = Session.User.Id;
            user.ModifiedDate = DateTime.Now;
            base.SetData();
        }
        public override bool IsDataValid()
        {
            base.IsDataValid();

            int errorCount = 0;
            errorCount += txt_Username.IsTextValid() ? 0 : 1;
            using (DatabaseContext ctx = new DatabaseContext())
            {
                ShowWaitForm();
                if (ctx.Users.Where(x => x.Username == txt_Username.Text.Trim() && x.Id != user.Id).Count() > 0)
                {
                    txt_Username.ErrorText = "This user already exists!";
                    errorCount++;
                }
                CloseWaitForm();
            }
            errorCount += txt_Password.IsTextValid() ? 0 : 1;
            errorCount += lkp_AccessProfile.IsEditValueNumberAndNotZero() ? 0 : 1;
            errorCount += lkp_SettingProfile.IsEditValueNumberAndNotZero() ? 0 : 1;
            errorCount += lkp_UserType.IsEditValueNumberAndNotZero() ? 0 : 1;
            return errorCount == 0;
        }


        public override void Save()
        {
            using (DatabaseContext ctx = new DatabaseContext())
            {
                if (IsNew)
                {
                    user.CreatedBy = Session.User.Id;
                    user.CreatedDate = DateTime.Now;
                }
                ctx.Users.AddOrUpdate(user);
                ShowWaitForm();
                ctx.SaveChanges();
                CloseWaitForm();
                base.Save(true);
            }
        }

        public override void Delete()
        {
            using (DatabaseContext ctx = new DatabaseContext())
            {
                ShowWaitForm();
                ctx.Users.Remove(user);
                ctx.SaveChanges();
                CloseWaitForm();
            }
            base.Delete();
        }
    }
}