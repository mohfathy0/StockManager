using DataModel;
using DevExpress.XtraEditors;
using StockManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager.Forms
{
    public partial class frm_Exception : frm_MasterEntry
    {
        ExceptionLog log;
        public frm_Exception()
        {

        }
        public frm_Exception(int Id)
        {
            InitializeComponent();
            this.Resize += Frm_Resize;

            btn_New.Visibility = btn_Delete.Visibility = btn_Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            using (DatabaseContext ctx = new DatabaseContext())
            {
                log = ctx.ExceptionLogs.SingleOrDefault(x => x.Id == Id);
            }
            GetData();
            dt_logDate.Properties.DisplayFormat.FormatString = "MMM/d/yyyy hh:mm tt";
        }
        private void Frm_Resize(object sender, EventArgs e)
        {
            ResizeEmptySpace(emptySpaceItemRight, emptySpaceItemLeft, emptySpaceItemTop);

        }
        public override void GetData()
        {
            txt_ActionType.EditValue = log.ActionType;
            txt_ClassName.EditValue = log.ClassName;
            txt_Exception.EditValue = log.exception;
            txt_LineNumber.EditValue = log.LineNumber;
            txt_logLevel.EditValue = log.logLevel;
            txt_logMessage.EditValue = log.logMessage;
            txt_logThread.EditValue = log.logThread;
            txt_MethodName.EditValue = log.MethodName;
            txt_RecordId.EditValue = log.RecordId;
            txt_RecordName.EditValue = log.RecordName;
            txt_ScreenName.EditValue = log.ScreenName;
            txt_UserId.EditValue = log.UserId;
            txt_UserName.EditValue = log.UserName;
            txt_logSource.EditValue = log.logSource;
            dt_logDate.EditValue = log.logDate;

            base.GetData();
        }


    }
}