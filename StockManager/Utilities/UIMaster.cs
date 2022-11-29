using DevExpress.LookAndFeel;
using DevExpress.Utils.Svg;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager.Utilities
{
    public static class UIMaster
    {

        public static Color GetRandomColor()
        {
            Random rnd = new Random();
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }
        public static void OpenFormInMain(Form form)
        {
            frm_MainForm frm = frm_MainForm.Instance;
            if (string.IsNullOrEmpty(form.Text) || form.Text == "(null)" || form.Tag == null)
                form.Text = Screens.GetScreens.Where(x => x.ScreenName == form.Name).FirstOrDefault()?.ScreenCaption;

            frm.OpenFormWithPermission(form);
        }
        public static void Shake(this Form frm)
        {
            var original = frm.Location;
            var rnd = new Random(1337);
            const int shake_amplitude = 10;
            for (int i = 0; i < 100; i++)
            {
                frm.Location = new Point(original.X + rnd.Next(-shake_amplitude, shake_amplitude),
                    original.Y + rnd.Next(-shake_amplitude, shake_amplitude));
                frm.Location = original;
            }
        }

        public static string ApplicationLayoutsPath
        {
            get
            {
                var myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                var appName = Application.ProductName;
                var path = Path.Combine(myDocuments, appName, "Layouts");
                Directory.CreateDirectory(path);
                return path;
            }
        }
        public static void SaveLayout(this GridView view, string ParentName)
        {
            var filePath = $"{ApplicationLayoutsPath}\\{ParentName}_{view.Name}";
            view.SaveLayoutToXml(filePath);
        }
        public static void RestoreLayout(this GridView view, string ParentName)
        {
            try
            {
                var filePath = $"{ApplicationLayoutsPath}\\{ParentName}_{view.Name}";
                if (File.Exists(filePath))
                {
                    view.RestoreLayoutFromXml(filePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void SaveLayout(this LayoutControl control, string ParentName)
        {
            var filePath = $"{ApplicationLayoutsPath}\\{ParentName}_{control.Name}";
            control.SaveLayoutToXml(filePath);
        }
        public static void RestoreLayout(this LayoutControl control, string ParentName)
        {
            try
            {
                var filePath = $"{ApplicationLayoutsPath}\\{ParentName}_{control.Name}";
                if (File.Exists(filePath))
                {
                    control.RestoreLayoutFromXml(filePath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void RenameTabInMain(Form form)
        {
            frm_MainForm.Instance.RenameTab(form);
        }

        public static void CloseTabInMain(Form form)
        {
            frm_MainForm.Instance.CloseTab(form);
        }
        public static void AddButtonToGroupHeader(this GridView view, SvgImage img, EventHandler eventHandler)
        {
            var customClass = new CustomHeaderButtonClass(view, img, eventHandler);
        }
        public class CustomHeaderButtonClass
        {
            public CustomHeaderButtonClass(GridView _view, SvgImage _img, EventHandler _eventHandler)
            {
                view = _view;
                svgImage = _img;
                Event = _eventHandler;
                view.CustomDrawGroupPanel += View_CustomDrawGroupPanel;
                view.MouseMove += View_MouseMove;
                view.Click += View_Click;
            }

            private void View_Click(object sender, EventArgs e)
            {
                DevExpress.Utils.DXMouseEventArgs ea = e as DevExpress.Utils.DXMouseEventArgs;
                if (r.Contains(ea.Location))
                    Event(sender, e);
            }

            private void View_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
            {
                IsInRectangle = r.Contains(e.Location);
                view.Invalidate();
            }

            GridView view;
            SvgImage svgImage;
            EventHandler Event;
            int svgSize = 16;
            bool IsInRectangle;
            Rectangle r;
            private void View_CustomDrawGroupPanel(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
            {

                //Brush brush2 = e.Cache.GetGradientBrush(e.Bounds, Color.LightYellow, Color.WhiteSmoke,
                //    System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                //e.Cache.FillRectangle(brush2, e.Bounds);
                int Additional = 0;
                var currentSkin = DevExpress.Skins.CommonSkins.GetSkin(UserLookAndFeel.Default);

                if (currentSkin.Name == "WXI")
                    Additional = 10;

                SvgBitmap bitmap = SvgBitmap.Create(svgImage);
                r = new Rectangle(e.Bounds.X + (e.Bounds.Width - Additional) - (svgSize * 3),
                   e.Bounds.Y + (((e.Bounds.Height - Additional) - svgSize) / 2), svgSize, svgSize);
                var palette = SvgPaletteHelper.GetSvgPalette(UserLookAndFeel.Default,
                    DevExpress.Utils.Drawing.ObjectState.Normal);
                e.Cache.DrawImage(bitmap.Render(palette), r);
                int thickness = IsInRectangle ? 2 : 1;
                int Offset = thickness + 1;
                e.Cache.DrawRectangle(r.X - Offset, r.Y - Offset, r.Width + (Offset * 2), r.Height + (Offset * 2), Color.Black, thickness);
                e.Handled = true;
            }


        }
    }
}
