using DataModel;
using StockManager.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Forms
{
    public static class Logger
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();


        public static void LogException(Exception e, System.Windows.Forms.Form form)
        {
            ExceptionLog ex = new ExceptionLog();
            frm_Master f = new frm_Master();
            ex.ActionType = frm_Master.ActionTypeException.NA.ToString();
            ex.ScreenName = form.Name;
            ex.RecordId = 0;
            ex.RecordName = "N/A";
            ex.ActionType = "N/A";
            if ((form.GetType().BaseType).BaseType == typeof(frm_Master))
            {
                f = ((frm_Master)form);
                ex.ScreenName = f.Name;
                ex.RecordId = f.recordId;
                ex.RecordName = f.recordName;
                var action = f.currentAction;
                ex.ActionType = action.ToString();
            }

            ex.UserId = Session.User.Id;
            ex.UserName = Session.User.Username;
            ex.LineNumber = Utilities.Master.GetLineNumber(e);
            ex.ClassName = Utilities.Master.GetClassName(e);
            ex.MethodName = e.TargetSite.Name;
            ex.logMessage = e.Message;
            LogException(ex.MethodName, ex.ClassName, ex.LineNumber, ex.logMessage, ex.UserName, ex.UserId, ex.RecordName, ex.RecordId, ex.ActionType, ex.ScreenName, e);
            f.CloseWaitForm();
            frm_ExceptionMessage exDialog = new frm_ExceptionMessage(ex.logMessage, e.StackTrace, f);
            exDialog.ShowDialog();
        }
        public static void LogException(string MethodName, string ClassName, int? LineNumber, string message, string UserName, int? UserId, string RecordName, int? RecordId, string CurrentAction, string ScreenName, Exception e)
        {

            log4net.GlobalContext.Properties["ScreenName"] = ScreenName;
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
