using DataModel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
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

namespace StockManager.Forms
{
    public partial class frm_ExceptionList : frm_MasterList
    {
        private int userId = Session.User.Id;
        private RepositoryItemDateEdit repoitemDateEdit = new RepositoryItemDateEdit();
        public frm_ExceptionList()
        {
            InitializeComponent();
            btn_New.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

        }

        private void frm_ExceptionList_Load(object sender, EventArgs e)
        {

            //gridControl.RepositoryItems.Add(repoitemDateEdit);
            //repoitemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //repoitemDateEdit.DisplayFormat.FormatString = "D";
            RefreshData();
            //gridView.Columns[nameof(ExceptionLog.logDate)].ColumnEdit = repoitemDateEdit;
            gridView.Columns[nameof(ExceptionLog.logDate)].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridView.Columns[nameof(ExceptionLog.logDate)].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
            //       gridView.Columns.Clear();
        }
        public override void gridViewDoubleClick(object sender, EventArgs e)
        {

            if (!gridView.ValidateGridDoubleClick(e))
                return;
            OpenRecord(sender as GridView);
            base.gridViewDoubleClick(sender, e);
        }

        public override void OpenRecord(GridView View)
        {
            base.OpenRecord(View);
            int Id = gridView.GetFocusedRowCellValue("Id").ToInt();
            string name = $"{gridView.GetFocusedRowCellValue(nameof(ExceptionLog.RecordName)).ToString()} Exception";

            UIMaster.OpenFormInMain(new frm_Exception(Id) { Tag = Id, Text = name });
        }
        public override void RefreshData()
        {

            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                gridControl.DataSource = ctx.ExceptionLogs.Where(x => x.UserId == userId && x.logDate >= MinimumDate).Select(x => new
                {
                    x.Id,
                    x.logDate,
                    x.UserName,
                    x.ClassName,
                    x.logSource,
                    x.logMessage,
                    x.RecordName
                }).ToList();
                gridView.Columns[nameof(ExceptionLog.Id)].Visible = false;
            }

            CloseWaitForm();
            base.RefreshData();
        }

    }
}