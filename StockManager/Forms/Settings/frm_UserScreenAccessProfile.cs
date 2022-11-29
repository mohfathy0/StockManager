using DataModel;
using StockManager.DAL;
using StockManager.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraTreeList;
using System.Data.Entity.Migrations;

namespace StockManager.Forms
{
    public partial class frm_UserScreenAccessProfile : frm_MasterEntry
    {
        UserAccessProfile profile;
        RepositoryItemCheckEdit repoCheck;

        public frm_UserScreenAccessProfile()
        {
            InitializeComponent();
            New();

        }
        public frm_UserScreenAccessProfile(int Id)
        {
            InitializeComponent();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                ShowWaitForm();
                profile = ctx.UserAccessProfiles.SingleOrDefault(x => x.Id == Id);
                CloseWaitForm();
            }
            NametextEdit.Text = profile.Name;
            recordId = profile.Id;
            recordName = profile.Name;
            GetData();
        }
        private void frm_UserScreenAccessProfile_Load(object sender, EventArgs e)
        {
            treeList1.CustomNodeCellEdit += TreeList1_CustomNodeCellEdit;
            treeList1.CellValueChanging += TreeList1_CellValueChanging;
            treeList1.CellValueChanged += TreeList1_CellValueChanged;
           
            treeList1.OptionsView.ShowIndicator = false;
            treeList1.OptionsView.NewItemRowPosition = TreeListNewItemRowPosition.Top;
            treeList1.KeyFieldName = nameof(ScreenAccessProfile.ScreenId);
            treeList1.ParentFieldName = nameof(ScreenAccessProfile.ParentScreenId);
            treeList1.Columns[nameof(ScreenAccessProfile.ScreenName)].Visible = false;
            treeList1.Columns[nameof(ScreenAccessProfile.ScreenName)].OptionsColumn.AllowEdit = false;
            treeList1.Columns[nameof(ScreenAccessProfile.ScreenCaption)].OptionsColumn.AllowEdit = false;

            treeList1.BestFitColumns();
            repoCheck = new RepositoryItemCheckEdit();
            repoCheck.CheckBoxOptions.Style = DevExpress.XtraEditors.Controls.CheckBoxStyle.SvgRadio2;
            treeList1.Columns[nameof(ScreenAccessProfile.CanAdd)].ColumnEdit = repoCheck;
            treeList1.Columns[nameof(ScreenAccessProfile.CanDelete)].ColumnEdit = repoCheck;
            treeList1.Columns[nameof(ScreenAccessProfile.CanEdit)].ColumnEdit = repoCheck;
            treeList1.Columns[nameof(ScreenAccessProfile.CanOpen)].ColumnEdit = repoCheck;
            treeList1.Columns[nameof(ScreenAccessProfile.CanPrint)].ColumnEdit = repoCheck;
            treeList1.Columns[nameof(ScreenAccessProfile.CanShow)].ColumnEdit = repoCheck;

            treeList1.Columns[nameof(ScreenAccessProfile.CanAdd)].Width = 25;
            treeList1.Columns[nameof(ScreenAccessProfile.CanDelete)].Width = 25;
            treeList1.Columns[nameof(ScreenAccessProfile.CanEdit)].Width = 25;
            treeList1.Columns[nameof(ScreenAccessProfile.CanOpen)].Width = 25;
            treeList1.Columns[nameof(ScreenAccessProfile.CanPrint)].Width = 25;
            treeList1.Columns[nameof(ScreenAccessProfile.CanShow)].Width = 25;

            treeList1.ExpandAll();
        }


        private void TreeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Node.Id >= 0 && e.ChangedByUser)
            {
                var row = treeList1.GetRow(e.Node.Id) as ScreenAccessProfile;
                if (e.Value.Equals(true))
                {
                    if (row.CanEdit || row.CanDelete || row.CanPrint || row.CanOpen || row.CanAdd)
                    {
                        if (null != e.Node.ParentNode)
                        {
                            var parentRow = treeList1.GetRow(e.Node.ParentNode.Id) as ScreenAccessProfile;
                            parentRow.CanShow = row.CanOpen = row.CanShow = true;
                        }
                    }
                }
                else
                {
                    if (row.CanShow == false || row.CanOpen == false)
                    {
                        // This is a child node "Screen an actual form"
                        if (null != e.Node.ParentNode)
                        {
                            row.CanOpen = row.CanEdit = row.CanDelete = row.CanPrint = row.CanAdd = false;
                        }
                        else
                        {
                            // This is a parent node "Screen menu item"
                            DevExpress.XtraTreeList.Nodes.TreeListNode node = e.Node;
                            System.Collections.IEnumerator en = e.Node.Nodes.GetEnumerator();
                            while (en.MoveNext())
                            {
                                var child = (DevExpress.XtraTreeList.Nodes.TreeListNode)en.Current;
                                var childRow = treeList1.GetRow(child.Id) as ScreenAccessProfile;
                                childRow.CanShow = childRow.CanOpen = childRow.CanEdit = childRow.CanDelete = childRow.CanPrint = childRow.CanAdd = false;
                            }
                        }
                    }
                }
            }
            treeList1.RefreshDataSource();
        }

        private string canShow = nameof(ScreenAccessProfile.CanShow);
        private void TreeList1_CellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            // Refresh Data and change entire column
            if (e.Node.Id == TreeList.NewItemNodeId && e.ChangedByUser)
            {
                foreach (var item in (treeList1.DataSource as List<ScreenAccessProfile>))
                {
                    var propertyInfo = item.GetType().GetProperty(e.Column.FieldName);
                    if (propertyInfo != null)  //this probably works. Yes it is
                    {
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanShow) && item.Actions.Contains(Master.Actions.Show))
                            propertyInfo.SetValue(item, e.Value, null);
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanOpen) && item.Actions.Contains(Master.Actions.Open))
                            propertyInfo.SetValue(item, e.Value, null);
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanAdd) && item.Actions.Contains(Master.Actions.Add))
                            propertyInfo.SetValue(item, e.Value, null);
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanDelete) && item.Actions.Contains(Master.Actions.Delete))
                            propertyInfo.SetValue(item, e.Value, null);
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanEdit) && item.Actions.Contains(Master.Actions.Edit))
                            propertyInfo.SetValue(item, e.Value, null);
                        if (propertyInfo.Name == nameof(ScreenAccessProfile.CanPrint) && item.Actions.Contains(Master.Actions.Print))
                            propertyInfo.SetValue(item, e.Value, null);

                    }
                }
            }
        }

        public override void New()
        {
            profile = new UserAccessProfile();
            base.New();
        }

        private void TreeList1_CustomNodeCellEdit(object sender, DevExpress.XtraTreeList.GetCustomNodeCellEditEventArgs e)
        {

            if (e.Node.Id >= 0)
            {
                var row = treeList1.GetRow(e.Node.Id) as ScreenAccessProfile;
                if (row != null)
                {
                    if (e.Column.FieldName == nameof(ScreenAccessProfile.CanAdd) && row.Actions.Contains(Master.Actions.Add) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();
                    }
                    else if (e.Column.FieldName == nameof(ScreenAccessProfile.CanDelete) && row.Actions.Contains(Master.Actions.Delete) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();

                    }
                    else if (e.Column.FieldName == nameof(ScreenAccessProfile.CanEdit) && row.Actions.Contains(Master.Actions.Edit) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();

                    }
                    else if (e.Column.FieldName == nameof(ScreenAccessProfile.CanOpen) && row.Actions.Contains(Master.Actions.Open) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();

                    }
                    else if (e.Column.FieldName == nameof(ScreenAccessProfile.CanPrint) && row.Actions.Contains(Master.Actions.Print) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();

                    }
                    else if (e.Column.FieldName == nameof(ScreenAccessProfile.CanShow) && row.Actions.Contains(Master.Actions.Show) == false)
                    {
                        e.RepositoryItem = new DevExpress.XtraEditors.Repository.RepositoryItem();

                    }
                }
            }
        }
        public override void GetData()
        {
            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                List<ScreenAccessProfile> data = (from s in Screens.GetScreens
                                                  from d in ctx.UserAccessProfileDetails
                                                  .Where(x => x.UserAccessProfileId == profile.Id && x.ScreenId == s.ScreenId).DefaultIfEmpty()
                                                  select new ScreenAccessProfile(s.ScreenName)
                                                  {
                                                      ScreenName = s.ScreenName,
                                                      ParentScreenId = s.ParentScreenId,
                                                      ScreenCaption = RenameScreen(s.ScreenCaption),
                                                      ScreenId = s.ScreenId,
                                                      Actions = s.Actions,
                                                      CanAdd = (d == null) ? false : d.CanAdd,
                                                      CanDelete = (d == null) ? false : d.CanDelete,
                                                      CanOpen = (d == null) ? false : d.CanOpen,
                                                      CanPrint = (d == null) ? false : d.CanPrint,
                                                      CanEdit = (d == null) ? false : d.CanEdit,
                                                      CanShow = (d == null) ? false : d.CanShow,
                                                  }).ToList();
                treeList1.DataSource = data;
                treeList1.Columns[nameof(ScreenAccessProfile.ScreenCaption)].Caption = "Menu item/Screen";
            }
            CloseWaitForm();
        }

        public override bool IsDataValid()
        {
            base.IsDataValid();

            return true;
        }
        public override void Save()
        {
            ShowWaitForm();
            using (DatabaseContext ctx = new DatabaseContext())
            {
                if (profile.Id == 0)
                    ctx.UserAccessProfiles.AddOrUpdate(profile);
                else
                    ctx.UserAccessProfiles.Attach(profile);

                profile.Name = NametextEdit.Text;
                ctx.SaveChanges();
                ctx.UserAccessProfileDetails.RemoveRange(ctx.UserAccessProfileDetails.Where(x => x.UserAccessProfileId == profile.Id));
                ctx.SaveChanges();
                var data = treeList1.DataSource as List<ScreenAccessProfile>;
                var dbData = data.Select(s => new UserAccessProfileDetail
                {
                    CanAdd = s.CanAdd,
                    CanDelete = s.CanDelete,
                    CanOpen = s.CanOpen,
                    CanPrint = s.CanPrint,
                    CanEdit = s.CanEdit,
                    CanShow = s.CanShow,
                    UserAccessProfileId = profile.Id,
                    ScreenId = s.ScreenId
                }).ToList();
                ctx.UserAccessProfileDetails.AddRange(dbData);
                ctx.SaveChanges();

                recordId = profile.Id;
                this.Text = recordName = profile.Name;
                CloseWaitForm();
                base.Save(true);

            }
        }

        private string RenameScreen(string name)
        {
            switch (name)
            {
                case "New user":
                    return "New/Edit user";
                case "New user settings profile":
                    return "New/Edit user settings profile";
                case "New screen access profile":
                    return "New/Edit screen access profile";
                default:
                    return name;
            }
        }

        public override void Delete()
        {
            throw new NotImplementedException();
            base.Delete();
        }
    }
}