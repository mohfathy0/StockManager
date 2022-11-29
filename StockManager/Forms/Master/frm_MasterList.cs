using DevExpress.XtraGrid.Views.Grid;
using StockManager.Properties;
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
    public partial class frm_MasterList : frm_MasterEntry
    {
        public frm_MasterList()
        {
            InitializeComponent();
            gridView.OptionsView.ShowAutoFilterRow = true;
            this.KeyDown += ParentFrmEntryScreen_KeyDown;
            gridView.OptionsBehavior.Editable = false;
            this.KeyPreview = true;
            this.Activated += Frm_MasterList_Activated;
            this.GotFocus += Frm_MasterList_Activated;
            gridView.DoubleClick += gridView_DoubleClick;
            //gridView.OptionsCustomization.UseAdvancedCustomizationForm = DevExpress.Utils.DefaultBoolean.True;
            //btn_Print.Visibility = btn_Excel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            currentAction = ActionTypeException.NA;
        }
        private void Frm_MasterList_Activated(object sender, EventArgs e)
        {
            gridView.Focus();
        }

        public void ParentFrmEntryScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.N) && btn_New.Enabled)
            {
                btn_New.PerformClick();
            }
            //else if ((e.Control && e.KeyCode == Keys.H) && btn_Help.Enabled)
            //{
            //    btn_Help.PerformClick();
            //}
            else if (e.KeyCode == Keys.F5)
            {
                RefreshData();
            }

        }

        private void btn_Help_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Help();
        }


        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            GridView v = sender as GridView;
            recordId = v.GetRowCellValue(v.FocusedRowHandle, "Id").ToInt();

            gridViewDoubleClick(sender, e);

        }
        public virtual void gridViewDoubleClick(object sender, EventArgs e)
        {

        }

        private void frm_MasterList_Load(object sender, EventArgs e)
        {
            gridView.OptionsView.EnableAppearanceOddRow = false;
            gridView.RestoreLayout(this.Name);
            gridView.PopupMenuShowing += GridView_PopupMenuShowing;
        }

        private void GridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            GridViewPopupMenuShowing(sender, e);
        }
        public virtual void GridViewPopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.Menu == null)
            // if (e.HitInfo.InRow || e.HitInfo.InRowCell)
            {
                return;
            }
            int rowHandle = e.HitInfo.RowHandle;
            if (rowHandle < 0)
                return;
            GridView view = (sender as GridView);

            var dxBtnNew = new DevExpress.Utils.Menu.DXMenuItem() { Caption = "New" };
            dxBtnNew.ImageOptions.SvgImage = Resources._new;
            dxBtnNew.Click += (senCN, argCN) =>
            {
                KeyEventArgs eargCN = new KeyEventArgs((Keys.Control | Keys.N));
                ParentFrmEntryScreen_KeyDown(null, eargCN);
            };
            var dxBtnOpen = new DevExpress.Utils.Menu.DXMenuItem() { Caption = "Open" };
            dxBtnOpen.ImageOptions.SvgImage = Resources.open;
            dxBtnOpen.Click += (senCO, argCO) =>
            {
                OpenRecord(view);
            };
            e.Menu.Items.Add(dxBtnOpen);

        }
        public virtual void OpenRecord(GridView View)
        {

        }

        private void btn_New_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            New();
        }
        public override void RefreshData()
        {
            try
            {
                base.RefreshData();
                txt_StateItem.Caption = gridView.RowCount + " Record(s) loaded";
            }
            catch (Exception ex)
            {
             //   Logger.LogException(ex, this);
            }
        }

        private void frm_MasterList_FormClosing(object sender, FormClosingEventArgs e)
        {
            gridView.RestoreLayout(this.Name);
        }

        private void btn_Refresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
              //  Logger.LogException(ex, this);
            }
        }
    }
}
