using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using StockManager.DAL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager.Utilities
{
    public static class Master
    {
        #region Error Messages
        public static readonly string ErrorRequired = "This field is required";
        public static readonly string ErrorDateBefore = "Start date cannot be greater than end date";
        public static readonly string ErrorInvalidDate = "Missing or invalid Date";
        public static readonly string ErrorItemAlreadyExist = "This item already exist";
        public static readonly string ErrorValueNeedToBeGreaterThanZero = "Value need to be greater than zero";
        #endregion
        public enum Actions { Show = 1, Open, Add, Edit, Delete, Print }
        public enum UserType { Admin = 1, User = 2 }
        public static List<ValueAndID> UserTypes = new List<ValueAndID>()
        {
            new ValueAndID(){Id=(int)UserType.Admin,Name=UserType.Admin.ToString()},
            new ValueAndID(){Id=(int)UserType.User,Name=UserType.User.ToString()},
        };

        public class ValueAndID
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
        public static void RenameTabInMain(Form form)
        {
            frm_MainForm.Instance.RenameTab(form);
        }




        #region Extension Methods
        public static void InitializeDateEditDateTime(this DateEdit edit)
        {
            edit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            //edit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            //edit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            //new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});

            edit.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            edit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            edit.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            edit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            edit.Properties.Mask.EditMask = "dd/MM/yyyy HH:mm";
            edit.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            edit.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            edit.Properties.AllowNullInput = DefaultBoolean.False;
            edit.MouseUp += Edit_Enter;
            //edit.Enter += Edit_Enter;
        }

        private static void Edit_Enter(object sender, EventArgs e)
        {
            DateEdit edit = sender as DateEdit;
            edit.ShowPopup();
        }
        public static void InitializeFixedPoint(this RepositoryItemSpinEdit spn)
        {
            spn.Mask.EditMask = "f";
            // spn.Mask.PlaceHolder = '_';
        }

        public static void InitializeNumber(this SpinEdit spn)
        {
            spn.Properties.Mask.EditMask = "D";
        }
        public static void InitializePercentage(this SpinEdit spn)
        {
            spn.Properties.Increment = 0.01m;
            spn.Properties.Mask.EditMask = "p";
            spn.Properties.Mask.UseMaskAsDisplayFormat = true;
            spn.Properties.MaxValue = 1;
        }
        public static void HideColumns(this GridLookUpEdit edit, string displayMember = "Name")
        {
            var cols = ((GridLookUpEdit)edit).Properties.View.Columns.Where(x => x.FieldName != displayMember);
            foreach (var col in cols)
            {
                col.Visible = false;
            }
        }
        public static void HideColumns(this BaseEdit edit, string displayMember = "Name")
        {
            var cols = ((LookUpEdit)edit).Properties.Columns.Where(x => x.FieldName != displayMember);
            foreach (var col in cols)
            {
                col.Visible = false;
            }
        }
        public static void HideColumns(this RepositoryItemGridLookUpEditBase edit, string displayMember = "Name")
        {
            RepositoryItemGridLookUpEdit GridEdit = edit as RepositoryItemGridLookUpEdit;
            GridEdit.PopulateViewColumns();
            var cols = (GridEdit).View.Columns.Where(x => x.FieldName != displayMember);
            foreach (var col in cols)
            {
                col.Visible = false;
            }
        }
        public static void HideColumns(this RepositoryItemGridLookUpEditBase edit, string[] ColumnsToShow, string displayMember = "Name")
        {
            RepositoryItemGridLookUpEdit GridEdit = edit as RepositoryItemGridLookUpEdit;
            GridEdit.PopulateViewColumns();
            var cols = (GridEdit).View.Columns.Where(x => x.FieldName != displayMember);
            int i = 1;
            foreach (var col in cols)
            {
                if (ColumnsToShow.Contains(col.FieldName))
                {
                    i++;
                    col.VisibleIndex = i;
                    //col.Visible = true ;
                    continue;
                }
                col.Visible = false;
            }
        }

        public static void HideColumns(this RepositoryItemLookUpEditBase edit, string displayMember = "Name")
        {
            ((RepositoryItemLookUpEdit)edit).PopulateColumns();
            var cols = ((RepositoryItemLookUpEdit)edit).Columns.Where(x => x.FieldName != displayMember);
            foreach (var col in cols)
            {
                col.Visible = false;
            }
        }


        public static void InitializeData(this GridLookUpEdit Glkp, object dataSource, string displayMember = "Name", string valueMember = "Id")
        {

            Glkp.Properties.DataSource = dataSource;
            Glkp.Properties.DisplayMember = displayMember;
            Glkp.Properties.ValueMember = valueMember;
            Glkp.Properties.PopulateViewColumns();
            Glkp.Properties.HideColumns(displayMember);
            Glkp.Properties.ValidateOnEnterKey = true;
            Glkp.Properties.AllowNullInput = DefaultBoolean.False;
            Glkp.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            Glkp.Properties.ImmediatePopup = true;
            Glkp.Properties.View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            Glkp.Properties.View.OptionsSelection.UseIndicatorForSelection = true;
            Glkp.Properties.View.OptionsView.ShowAutoFilterRow = true;
            Glkp.Properties.View.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            Glkp.Properties.NullText = "";

        }
        //public static void InitializeData(this LookUpEdit lkp, object dataSource)
        //{
        //    InitializeData(lkp, dataSource, "Name", "Id");
        //}
        public static void InitializeData(this LookUpEdit lkp, object dataSource, string displayMember = "Name", string valueMember = "Id")
        {
            lkp.Properties.DataSource = dataSource;
            lkp.Properties.DisplayMember = displayMember;
            lkp.Properties.ValueMember = valueMember;
            lkp.Properties.ForceInitialize();
            lkp.Properties.PopulateColumns();
            lkp.Properties.ShowHeader = false;
            lkp.Properties.NullText = "";
            if (lkp.Properties.Columns.Count > 1)
            {
                HideColumns(lkp, displayMember);
            }
        }
        //public static void InitializeData(this LookUpEdit lkp, object dataSource, string displayMember)
        //{
        //    InitializeData(lkp, dataSource, displayMember, "Id");
        //}
        //public static void InitializeData(this LookUpEdit lkp, object dataSource, string valueMember, object passNull)
        //{
        //    InitializeData(lkp, dataSource, "Name", valueMember);
        //}
        public static void InitializeData(this LookUpEdit lkp, List<ValueAndID> datasouce)
        {
            lkp.Properties.DataSource = datasouce;
            lkp.Properties.NullValuePrompt = "";
            lkp.Properties.DisplayMember = "Name";
            lkp.Properties.ValueMember = "Id";
            lkp.Properties.Columns.Clear();
            lkp.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo()
            {
                FieldName = "Name",

            });
            lkp.Properties.ShowHeader = false;

        }

        public static DateTimeOffset? SetOffset(this DateEdit dateEdit, DateTime? dt)
        {
            if (dt == null)
            {
                return null;
            }
            dt = DateTime.SpecifyKind(dt.Value.Date, DateTimeKind.Local);
            DateTimeOffset utcTime2 = dt.Value.Date;
            dateEdit.EditValue = dt;
            return utcTime2;
        }

        public static DateTime? GetLocal(this DateEdit dateEdit)
        {
            DateTimeOffset? DT = dateEdit.EditValue as DateTimeOffset?;
            if (DT == null)
                return null;
            else
                return TimeZone.CurrentTimeZone.ToLocalTime(DT.Value.DateTime);
        }

        public static void InitializeData(this RepositoryItemLookUpEditBase repo, object dataSource, GridColumn column, DevExpress.XtraGrid.GridControl grid)
        {
            InitializeData(repo, dataSource, column, grid, "Name", "Id");
        }
        public static void InitializeData(this RepositoryItemLookUpEditBase repo, object dataSource, GridColumn column, DevExpress.XtraGrid.GridControl grid, string displayMember, string valueMember)
        {
            if (repo == null)
            {
                repo = new RepositoryItemLookUpEdit();
            }

            repo.DataSource = dataSource;
            column.ColumnEdit = repo;
            repo.ValueMember = valueMember;
            repo.DisplayMember = displayMember;
            repo.NullText = string.Empty;
            repo.BestFitMode = BestFitMode.BestFitResizePopup;
            if (grid != null)
                grid.RepositoryItems.Add(repo);

            if (repo is RepositoryItemGridLookUpEdit)
                HideColumns(repo as RepositoryItemGridLookUpEdit, displayMember);
            else
                HideColumns(repo, displayMember);
        }


        public static void SetSpinEditPercentage(this SpinEdit spn)
        {
            spn.Properties.Increment = 0.01m;
            spn.Properties.Mask.EditMask = "p";
            spn.Properties.Mask.UseMaskAsDisplayFormat = true;
            spn.Properties.MaxValue = 1;
        }
        public static void SetSpinEditPercentage(this RepositoryItemSpinEdit spn)
        {
            spn.Increment = 0.01m;
            spn.Mask.EditMask = "p";
            spn.Mask.UseMaskAsDisplayFormat = true;
            spn.MaxValue = 1;
        }
        public static void HideGridColumns(GridView view, string[] ShowColumn, object Model)
        {
            view.PopulateColumns(Model);
            HideGridColumns(view, ShowColumn);
        }
        public static void HideGridColumns(this GridView view, string[] ShowColumns)
        {
            var cols = view.Columns;
            cols.ToList().ForEach(X => X.Visible = false);

            foreach (var colname in ShowColumns)
            {

                if (ShowColumns.Contains(colname))
                {
                    cols.Where(x => x.FieldName == colname).FirstOrDefault().VisibleIndex = ShowColumns.ToList().IndexOf(colname);
                    continue;
                }
            }

            //for (int i = 0; i < cols.Count; i++)
            //{
            //    if (ShowColumns.Contains(cols[i].FieldName))
            //    {
            //        cols[i].VisibleIndex = ShowColumns.ToList().IndexOf(cols[i].FieldName);
            //        continue;
            //    }
            //    cols[i].Visible = false;
            //}
            view.BestFitColumns();
        }
        public static bool IsGridViewNotEmpty(this GridView view, string ErrorFieldName)
        {
            if (view.RowCount == 0)
            {
                view.AddNewRow();
                view.SetColumnError(view.Columns[ErrorFieldName], ErrorRequired);
                return false;
            }
            return true;
        }
        public static bool IsRowsMoreThanZero(this GridView view, string ErrorFieldName, int count)
        {
            if (view.RowCount < count)
            {
                view.AddNewRow();
                //var row = view.GetRow(DevExpress.XtraGrid.GridControl.NewItemRowHandle) as System.Data.DataRow;
                //row.SetColumnError(ErrorFieldName, $"At least {count} segments required");
                view.SetColumnError(view.Columns[ErrorFieldName], $"At least {count} segments required", DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);

                return false;
            }
            return true;
        }

        public static bool IsRowsMoreThanZero(this GridView view, int count = 1)
        {
            return IsRowsMoreThanZero(view, view.VisibleColumns[0].FieldName, count);
        }
        public static int FindRowHandleByRowObject(this GridView view, Object row)
        {
            if (row != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (row.Equals(view.GetRow(i)))
                        return i;
                }
            }
            return DevExpress.XtraGrid.GridControl.InvalidRowHandle;
        }
        public static void SortGridByColumn(this GridView view, string ColumnName)
        {
            view.BeginSort();
            try
            {
                view.Columns[ColumnName].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            finally
            {
                view.EndSort();
            }
        }
        public static bool IsTextValid(this TextEdit txt)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                txt.ErrorText = ErrorRequired;
                return false;
            }
            return true;
        }
        public static bool IsTextValid(this ButtonEdit txt)
        {
            if (txt.Text.Trim() == string.Empty)
            {
                txt.ErrorText = ErrorRequired;
                return false;
            }
            return true;
        }
        public static bool IsValueNotLessThanZero(this SpinEdit edit, bool SetError = true)
        {
            if (SetError && edit.Value < 0)
                edit.ErrorText = "Value cannot be less than zero";
            else
                edit.ErrorText = "";
            return edit.Value >= 0;
        }
        public static bool IsValueGreaterThanZero(this RadioGroup edit, bool SetError = true)
        {
            if (SetError && edit.EditValue.ToInt() <= 0)
                edit.ErrorText = "Invalid value";
            else
                edit.ErrorText = "";
            return edit.EditValue.ToDouble() > 0;
        }
        public static bool IsValueGreaterThanZero(this CalcEdit edit, bool SetError = true)
        {
            if (SetError && edit.Value <= 0)
                edit.ErrorText = ErrorValueNeedToBeGreaterThanZero;
            else
                edit.ErrorText = "";
            return edit.Value > 0;
        }
        public static bool IsValueGreaterThanZero(this SpinEdit edit, bool SetError = true)
        {
            if (SetError && edit.Value <= 0)
                edit.ErrorText = ErrorValueNeedToBeGreaterThanZero;
            else
                edit.ErrorText = "";
            return edit.Value > 0;
        }

        public static bool IsEditValueNumberAndNotZero(this LookUpEditBase lkp)
        {
            //if (string.IsNullOrWhiteSpace(lkp.Text))
            //{
            //    lkp.ErrorText = ErrorRequired;
            //    return false;
            //}
            var val = lkp.EditValue;
            if ((val is DBNull))
            {
                lkp.ErrorText = ErrorRequired;
                return false;
            }
            if ((val is int || val is byte || val is double || val is float || val is decimal
           //     ||
           //val is Master.StaffType || val is Master.UserType || val is Master.ShippingAgentType ||
           //val is Master.VehicleType || val is Master.WarningLevel || val is Master.PaymentMethod ||
           //val is Master.PartnerType
           )
           & (Convert.ToInt32(val) != 0))
            {
                return true;
            }
            lkp.ErrorText = ErrorRequired;
            return false;

        }
        public static bool IsEditValueOfTypeInt(this LookUpEditBase lkp)
        {
            if (lkp.EditValue != null && int.TryParse(lkp.EditValue.ToString(), out int num))
            {
                return true;
            }
            return false;
        }
        public static bool IsDateValid(this DateEdit dt)
        {
            if (dt.DateTime.Year < 1950)
            {
                dt.ErrorText = ErrorInvalidDate;
                return false;
            }
            return true;
        }
        public static bool IsGreaterThan(this DateEdit Start, DateEdit End)
        {
            if (Start.DateTime > End.DateTime)
            {
                Start.ErrorText = ErrorDateBefore;
                End.ErrorText = ErrorDateBefore;
                return false;
            }
            Start.ErrorText = "";
            End.ErrorText = "";
            return true;
        }

        ////Copied
        //public static T FromByteArray<T>(byte[] data)
        //{
        //    if (data == null)
        //        return default(T);
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    using (MemoryStream stream = new MemoryStream(data))
        //    {
        //        object obj = (T)formatter.Deserialize(stream);
        //        return (T)obj;
        //    }
        //}
        ////Copied
        //public static byte[] ToByteArray<T>(T obj)
        //{
        //    if (obj == null)
        //        return null;
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        formatter.Serialize(stream, obj);
        //        if (stream.Length == 0)
        //            return new byte[1];
        //        return stream.ToArray();
        //    }
        //}

        public static byte[] GetPropertyValue(string propertyName, int profileId)
        {
            using (var db = new DatabaseContext())
            {
                var prop = db.UserSettingsProfileProperties.SingleOrDefault(x => x.ProfileId == profileId && x.PropertyName == propertyName);
                if (prop == null)
                    return null;
                return prop.PropertyValue.ToArray();
            }
        }

        public static byte[] GetGlobalSettingValue(string propertyName)
        {
            using (var db = new DatabaseContext())
            {
                var prop = Session.globalSettings.SingleOrDefault(x => x.SettingName == propertyName);
                if (prop == null || prop.SettingValue == null)
                    return null;

                return prop.SettingValue.ToArray();
            }
        }

        public static int GetGlobalSettingId(string propertyName)
        {
            using (var db = new DatabaseContext())
            {
                var prop = Session.globalSettings.SingleOrDefault(x => x.SettingName == propertyName);
                if (prop == null)
                    return 0;
                return prop.Id;
            }
        }
        ////Copied
        //public static string GetCallerName([CallerMemberName] string callerName = "")
        //{
        //    return callerName;
        //}
        public static bool ValidateGridDoubleClick(this TileView sender, EventArgs e)
        {
            TileView view = sender as TileView;
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            TileViewHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.ItemInfo != null)
            {
                TileItemElementViewInfo elementInfo = info.ItemInfo.Elements.FirstOrDefault(t => t.EntireElementBounds.Contains(ea.Location));
                if (elementInfo != null)
                {
                    return true;
                }

            }
            return false;
        }
        public static bool ValidateGridDoubleClick(this GridView sender, EventArgs e)
        {
            GridView view = sender as GridView;
            DXMouseEventArgs ea = e as DXMouseEventArgs;
            GridHitInfo info = view.CalcHitInfo(ea.Location);
            if (info.InRow || info.InRowCell)
            {
                return true;
            }
            return false;
        }
        //public static bool ValidateGridDoubleClick(this TreeList sender, EventArgs e)
        //{
        //    TreeList view = sender as TreeList;
        //    DXMouseEventArgs ea = e as DXMouseEventArgs;
        //    TreeListHitInfo info = view.CalcHitInfo(ea.Location);
        //    if (info.InRow || info.InRowCell)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
        ////Copied
        //public static decimal ToDecimal(this object s)
        //{
        //    if (s is string)
        //        return 0;
        //    return Convert.ToDecimal(s);
        //}
        ////Copied
        //public static double ToDouble(this object s)
        //{
        //    if (s is string)
        //        return 0;
        //    return Math.Round(Convert.ToDouble(s), 2);
        //}
        ////Copied
        //public static int ToInt(this object s)
        //{
        //    if (s is string)
        //        return 0;
        //    return Convert.ToInt32(s);
        //}
        ////Copied
        //public static byte ToByte(this object s)
        //{
        //    if (s is string)
        //        return ((byte)s);
        //    if (s.ToInt() < 0)
        //        return 0;
        //    return Convert.ToByte(s);
        //}
        ////Copied
        //// Linq Query extensions
        //public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}
        ////Copied
        //public static IEnumerable<TSource> WhereIf<TSource>(this IEnumerable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}

        public static void SetCultureCurrency(this BaseEdit edit, string CurrencyCultureInfo = "")
        {
            var editor = edit as CalcEdit;
            editor.Properties.Mask.Culture = GetCultureInfo(CurrencyCultureInfo);
            editor.Properties.Mask.EditMask = "c";
            editor.Properties.Mask.UseMaskAsDisplayFormat = true;
        }
        public static CultureInfo GetCultureInfo(string CurrencyCultureInfo = "")
        {
            if (string.IsNullOrWhiteSpace(CurrencyCultureInfo))
                CurrencyCultureInfo = Session.Global.General.DefaultCurrencyCultureInfo;
            var cultureInfo = new System.Globalization.CultureInfo(CurrencyCultureInfo);
            cultureInfo.NumberFormat.CurrencyDecimalSeparator = Session.Global.General.DefaultCurrencyDecimalSeperator;
            cultureInfo.NumberFormat.CurrencyGroupSeparator = Session.Global.General.DefaultCurrencyGroupSeparator;
            cultureInfo.NumberFormat.NumberDecimalDigits = Session.Global.General.DefaultCurrencyNumberDecimalDigits.Value;
            return cultureInfo;
        }

        //public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Func<TSource, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}

        //public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Func<TSource, int, bool> predicate)
        //{
        //    if (condition)
        //        return source.Where(predicate);
        //    else
        //        return source;
        //}
        #endregion


        public static int GetLineNumber(Exception e)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(e, true);
            return trace.GetFrame(0).GetFileLineNumber();
        }
        public static string GetClassName(Exception e)
        {
            System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(e, true);
            return trace.GetFrame(0).GetMethod().ReflectedType.FullName;
        }
    }


}
