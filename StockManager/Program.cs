using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager
{
    internal static class Program
    {
        private static readonly log4net.ILog log = Utilities.LogHelper.GetLogger();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());
            frm_Login frm = frm_Login.Instance;

            if (Debugger.IsAttached)
            {
                frm.txt_Password.Text = frm.txt_Username.Text = "admin";
                frm.Show();
                frm.btn_Login.PerformClick();
            }
            else
            {
                frm.Show();
            }
            Application.Run();

        }
        public static void LogException(string MethodName, string ClassName, int LineNumber, string message, string UserName, int UserId, string RecordName, int RecordId, string CurrentAction, Exception e)
        {
            log4net.GlobalContext.Properties["ScreenName"] = "Program.cs";
            log4net.GlobalContext.Properties["MethodName"] = MethodName;
            log4net.GlobalContext.Properties["UserName"] = UserName;
            log4net.GlobalContext.Properties["UserId"] = UserId;
            log4net.GlobalContext.Properties["RecordName"] = RecordName;
            log4net.GlobalContext.Properties["RecordId"] = RecordId;
            log4net.GlobalContext.Properties["ActionType"] = CurrentAction;
            log4net.GlobalContext.Properties["ClassName"] = ClassName;
            log4net.GlobalContext.Properties["LineNumber"] = LineNumber;
            if (e != null)
                log.Fatal($"Message: {message}", e);
            else
                log.Fatal($"Message: {message}");


        }
    }
}
