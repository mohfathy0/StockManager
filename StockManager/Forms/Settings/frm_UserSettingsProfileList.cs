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
    public partial class frm_UserSettingsProfileList : frm_MasterList
    {
        public frm_UserSettingsProfileList()
        {
            InitializeComponent();
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
            var Id = gridView.GetFocusedRowCellValue(nameof(UserSettingsProfile.Id)).ToInt();
            string name = gridView.GetFocusedRowCellValue(nameof(UserSettingsProfile.Name)).ToString();
            UIMaster.OpenFormInMain(new frm_UserSettingsProfile(Id) { Tag = name, Text = name });

        }
        public override void RefreshData()
        {
            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                gridControl.DataSource = ctx.UserSettingsProfiles.ToList();
            }
            CloseWaitForm();
            base.RefreshData();
        }
        public override void New()
        {
            UIMaster.OpenFormInMain(new frm_UserSettingsProfile());
            base.New();
        }
    }
}