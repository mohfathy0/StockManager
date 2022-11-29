using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using StockManager.Utilities;
using DataModel;
using StockManager.DAL;

namespace StockManager.Forms
{
    public partial class frm_Master : DevExpress.XtraEditors.XtraForm
    {
        public ActionTypeException currentAction = ActionTypeException.NA;
        public static DateTime MinimumDate { get { return DateTime.Today.AddDays(-Session.userSettings.General.MinimumBackwardSearchInDays); } }

        public bool IsNew = false;
       public enum ActionType { Add, Edit, Delete, Print }
        public enum ActionTypeException { Add = ActionType.Add, Edit = ActionType.Edit, Delete = ActionType.Delete, Print = ActionType.Print, List, NA }
        public string recordName = "N/A";
        public int recordId;
        public Color OddRowColor { get => Color.WhiteSmoke; }
        public Color EvenRowColor { get => Color.FromArgb(200, 255, 249, 196); }

        // protected override FormShowMode ShowMode { get { return FormShowMode.AfterInitialization; } }

        public frm_Master()
        {
            InitializeComponent();
        //    waitScreenManager.SplashFormStartPosition = DevExpress.XtraSplashScreen.SplashFormStartPosition.CenterScreen;
            //  MinimumDate = DateTime.Today.AddYears(-Session.Defaults.DefaultSearchBackDays);
            this.KeyPreview = true;
        }




        private void Frm_Master_Load(object sender, EventArgs e)
        {
            if (Controls["layoutControl1"] != null)
                foreach (Control control in Controls["layoutControl1"].Controls)
                {
                    if (control.GetType() == typeof(TextEdit))
                    {
                        (control as TextEdit).LostFocus += LKPEdit_LostFocus;
                        TextEditRemoveWhiteSpaceOrNull(control);
                    }
                    else if (control.GetType() == typeof(LookUpEdit))
                    {
                        (control as LookUpEdit).Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(Keys.Control | Keys.Space);
                        (control as LookUpEdit).LostFocus += LKPEdit_LostFocus;
                    }
                    else if (control.GetType() == typeof(DateEdit))
                    {
                        (control as DateEdit).LostFocus += LKPEdit_LostFocus;
                    }
                }
            SetCustomizationMode();
            this.LookAndFeel.UseDefaultLookAndFeel = true;
            this.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
        }

        private void LKPEdit_LostFocus(object sender, EventArgs e)
        {
            ((BaseEdit)sender).DoValidate();
        }

        private void dateedit_LostFocus(object sender, EventArgs e)
        {
            ((DateEdit)sender).DoValidate();
        }

        public virtual void ReadUserSettings()
        {

        }
        public static bool CheckActionAuthorization(string formName, Master.Actions actions, User user = null)
        {
            if (user == null)
                user = Session.User;
            if (user.UserType == (byte) Master.UserType.Admin)
                return true;
            else
            {
                var Screen = Utilities.Session.ScreenAccess.SingleOrDefault(x => x.ScreenName == formName);
                bool flag = true;
                if (Screen != null)
                    switch (actions)
                    {
                        case Master.Actions.Open:
                            flag = Screen.CanOpen;
                            break;

                        case Master.Actions.Add:
                            flag = Screen.CanAdd;
                            break;
                        case Master.Actions.Edit:
                            flag = Screen.CanEdit;
                            break;
                        case Master.Actions.Delete:
                            flag = Screen.CanDelete;
                            break;
                        case Master.Actions.Print:
                            flag = Screen.CanPrint;
                            break;
                    }
                if (flag == false)
                    XtraMessageBox.Show("User does not have permission to access this screen", "Access denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return flag;
            }
        }
        public void InsertUserAction(ActionType actionType)
        {
            ShowWaitForm();
            InsertUserAction(actionType, this.recordId, this.recordName, this.Name);
            CloseWaitForm();
        }
        public static void InsertUserAction(ActionType actionType, int recordId, string recordName, string ScreenName)
        {
            using (var ctx = new DatabaseContext())
            {

                int screenID = -1;
                var screen = Utilities.Screens.GetScreens.SingleOrDefault(x => x.ScreenName == ScreenName);
                if (screen != null) screenID = screen.ScreenId;
                ctx.ActionLogs.Add(new ActionLog()
                {
                    UserId = Session.User.Id,
                    RecordId = recordId,
                    RecordName = recordName,
                    ActionType = (byte)actionType,
                    ActionDate = DateTime.Now,
                    ScreenId = screenID,
                });
                ctx.SaveChanges();
            }
        }
        public void ShowWaitForm()
        {
            //if (waitScreenManager.IsSplashFormVisible)
            //{
            //    waitScreenManager.CloseWaitForm();
            //    waitScreenManager.ShowWaitForm();
            //}
            //else
            //{
            //    waitScreenManager.ShowWaitForm();
            //}
        }
        public void CloseWaitForm()
        {
            //if (waitScreenManager.IsSplashFormVisible)
            //{
            //    waitScreenManager.CloseWaitForm();
            //}
        }
        public virtual void New()
        {
            IsNew = true;
            btn_Delete.Enabled = false;
            GetData();
        }
        public virtual void Save()
        {
            CloseWaitForm();
            XtraMessageBox.Show("Data saved!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Master.RenameTabInMain(this);
            IsNew = false;
        }

        public virtual void Save(bool RenameScreenToFormText = true)
        {
            XtraMessageBox.Show("Data saved!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (RenameScreenToFormText)
                Master.RenameTabInMain(this);
            IsNew = false;
        }
        public virtual void RefreshData()
        {

        }
        public virtual void Delete()
        {
            CloseWaitForm();
            XtraMessageBox.Show("Record Deleted!", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public virtual void Excel()
        {

        }
        public virtual void CustomizeLayout()
        {
            if (null != Controls["layoutControl1"])
                (Controls["layoutControl1"] as LayoutControl).ShowCustomizationForm();
        }
        public virtual void GetData()
        {

        }
        public virtual void SetData()
        {

        }
        public virtual bool IsDataValid()
        {
            SetData();
            return true;
        }
        public virtual void Print()
        {

        }
        public virtual void Help()
        {

        }
        public void ErrorMessage(string message)
        {
            XtraMessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void PermissionErrorMessage()
        {
            XtraMessageBox.Show("Your access level does not permit this operation!", "Permission denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public DialogResult QuestionUser(string AreYouSureOf, string ConfirmationType)
        {
            return XtraMessageBox.Show($"Are you sure {AreYouSureOf}?", $"{ConfirmationType} confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        private void TextEdit_LostFocus(object sender, EventArgs e)
        {
            (sender as TextEdit).DoValidate();
            TextEditRemoveWhiteSpaceOrNull(sender);
        }
        private void TextEditRemoveWhiteSpaceOrNull(object sender)
        {
            TextEdit edit = sender as TextEdit;
            edit.Text = edit.Text.Trim();

            if (string.IsNullOrWhiteSpace(edit.Text))
                edit.EditValue = null;
        }

        public void LockRecord()
        {
            DisableControls();
        }
        public void LockControls()
        {
            DisableControls();
        }
        public void DisableControls()
        {
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(LayoutControl))
                {
                    LayoutControl layout = control as LayoutControl;
                    foreach (Control item in layout.Controls)
                    {
                        item.Enabled = false;
                    }
                }
            }
        }
        private void SetCustomizationMode()
        {

            foreach (Control control in Controls)
            {

                if (control.GetType() == typeof(LayoutControl))
                {
                    LayoutControl layout = control as LayoutControl;
                    layout.CustomizationMode = DevExpress.XtraLayout.CustomizationModes.Quick;
                    layout.OptionsCustomizationForm.DefaultPage = DevExpress.XtraLayout.CustomizationPage.LayoutTreeView;
                    layout.OptionsCustomizationForm.ShowSaveButton =
                        layout.OptionsCustomizationForm.ShowLoadButton = false;
                    layout.OptionsCustomizationForm.ShowPropertyGrid = true;
                    foreach (BaseLayoutItem item in layout.Items)
                    {
                        item.AllowHide = false;
                    }
                }
            }
        }

        private void btn_New_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                New();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, this);
            }
        }

        private void btn_Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (QuestionUser("you want to Save", "Save") == DialogResult.Yes)
                    if (CheckActionAuthorization(this.Name, IsNew ? Master.Actions.Add : Master.Actions.Edit))
                    {
                        if (IsDataValid())
                        {
                            Save();
                            InsertUserAction((IsNew) ? ActionType.Add : ActionType.Edit);
                            currentAction = ((IsNew) ? ActionTypeException.Add : ActionTypeException.Edit);
                        }
                    }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, this);
            }
        }

        private void btn_Delete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (QuestionUser("you want to delete", "Delete") == DialogResult.Yes)
                    if (CheckActionAuthorization(this.Name, Master.Actions.Delete))
                    {
                        ShowWaitForm();
                        Delete();
                        currentAction = ActionTypeException.Delete;
                        InsertUserAction(ActionType.Delete);
                    }
                btn_New.PerformClick();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, this);
            }
        }

        private void btn_Print_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (CheckActionAuthorization(this.Name, Master.Actions.Print))
                {
                    Print();
                    currentAction = ActionTypeException.Print;
                    InsertUserAction(ActionType.Print);
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex, this);
            }
        }
    }
}