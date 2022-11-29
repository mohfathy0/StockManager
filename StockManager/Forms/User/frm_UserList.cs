using DataModel;
using DevExpress.XtraEditors;
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
    public partial class frm_UserList : frm_MasterList
    {
        public frm_UserList()
        {
            InitializeComponent();
            RefreshData();
            gridView.Columns[nameof(User.Id)].Visible =
            gridView.Columns[nameof(User.Password)].Visible =
            gridView.Columns[nameof(User.UserType)].Visible =
            gridView.Columns[nameof(User.SettingsProfileId)].Visible =
            gridView.Columns[nameof(User.ScreenProfileId)].Visible =
            gridView.Columns[nameof(User.CreatedBy)].Visible =
            gridView.Columns[nameof(User.CreatedDate)].Visible =
            gridView.Columns[nameof(User.ModifiedBy)].Visible =
            gridView.Columns[nameof(User.ModifiedDate)].Visible = false;
            gridView.Columns[nameof(User.Inactive)].MaxWidth = 100;
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
            var Id = gridView.GetFocusedRowCellValue(nameof(User.Id)).ToInt();
            string name = gridView.GetFocusedRowCellValue(nameof(User.Username)).ToString();
            UIMaster.OpenFormInMain(new frm_User(Id) { Tag = name, Text = name });

        }
        public override void RefreshData()
        {
            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                gridControl.DataSource = ctx.Users.ToList();
            }
            CloseWaitForm();
            base.RefreshData();
        }
        public override void New()
        {
            UIMaster.OpenFormInMain(new frm_User());
            base.New();
        }
    }
}