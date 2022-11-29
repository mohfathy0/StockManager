using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using StockManager.DAL;
using StockManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager
{
    public partial class frm_Login : DevExpress.XtraEditors.XtraForm
    {
        private static frm_Login _instance;
        public static frm_Login Instance
        {
            get
            {
                if (_instance == null || _instance.IsDisposed)
                {
                    if (null != Application.OpenForms["frm_Login"])
                    {
                        if (!Application.OpenForms["frm_Login"].IsDisposed)
                        {
                            _instance = (Application.OpenForms["frm_Login"] as frm_Login);
                        }
                    }
                    else
                        _instance = new frm_Login();
                }
                return _instance;
            }
        }

        public frm_Login()
        {
            InitializeComponent(); 
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosing += Frm_Login_FormClosing;
        }

        private void Frm_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            txt_Username.Text = txt_Password.Text = string.Empty;
            txt_Username.Focus();
            if (Application.OpenForms[nameof(frm_MainForm)] == null)
                Application.Exit();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            ShowWaitForm();
            //var configuration = new Migrations.Configuration();
            //var migrator = new System.Data.Entity.Migrations.DbMigrator(configuration);
            //migrator.Update();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                var username = txt_Username.Text;
                var password = txt_Password.Text;
                if (!txt_Username.IsTextValid() || !txt_Password.IsTextValid())
                {
                    CloseWaitForm();
                    return;
                }

                var user = ctx.Users.SingleOrDefault(x => x.Username == username);
                if (user == null)
                {
                    ShowMsg("Invalid username or password");
                    return;
                }
                else if (user.Inactive)
                {
                    ShowMsg("Inactive account please contact administrator");
                    return;
                }
                var passwordHash = user.Password;
                var hasher = new Liphsoft.Crypto.Argon2.PasswordHasher();
                if (!hasher.Verify(passwordHash, password))
                {
                    ShowMsg("Invalid username or password");
                    return;
                }
                else
                {
                    txt_Username.Text = txt_Password.Text = string.Empty;
                    txt_Username.Focus();
                    CloseWaitForm();
                    this.Hide();
                    SplashScreenManager.ShowForm(this, typeof(frm_SplashScreen));

                    Session.SetUser(user);
                    Type t = typeof(Utilities.Session);
                    var properties = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                    foreach (var item in properties)
                    {
                        var obj = item.GetValue(null);
                    }
                    // Loading here

                    frm_MainForm.Instance.Show();
                    frm_MainForm.Instance.InitializeAccordionControl();
                    SplashScreenManager.CloseForm();

                }
            }
        }

        private void ShowMsg(string message)
        {
            CloseWaitForm();
            lbl_msg.Text = message;
            lbl_msg.AppearanceItemCaption.ForeColor = DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            this.Shake();
        }

        public void ShowWaitForm()
        {
            if (waitScreenManager.IsSplashFormVisible)
            {
                waitScreenManager.CloseWaitForm();
                waitScreenManager.ShowWaitForm();
            }
            else
            {
                waitScreenManager.ShowWaitForm();
            }
        }

        public void CloseWaitForm()
        {
            if (waitScreenManager.IsSplashFormVisible)
            {
                waitScreenManager.CloseWaitForm();
            }
        }
    }
}