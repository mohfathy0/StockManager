using StockManager.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Utilities
{
    public class ScreenAccessProfile
    {
        public List<Master.Actions> Actions { get; set; }
        public static int MaxId = 1;
        public int ParentScreenId { get; set; }
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
        public string ScreenCaption { get; set; }
        public bool CanShow { get; set; }
        public bool CanOpen { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }

        public ScreenAccessProfile(string name, ScreenAccessProfile parent = null)
        {
            if (parent != null)
                ParentScreenId = parent.ScreenId;
            else
                ParentScreenId = 0;
            ScreenName = name;
            ScreenId = MaxId++;
            Actions = new List<Master.Actions> {
            Master.Actions.Add,
            Master.Actions.Edit,
            Master.Actions.Delete,
            Master.Actions.Print,
            Master.Actions.Show,
            Master.Actions.Open,
            };
        }

      
       
    }
    public static class Screens
    {
        public static ScreenAccessProfile Users = new ScreenAccessProfile("elm_User")
        {
            Actions = new List<Master.Actions>() { Master.Actions.Show },
            ScreenCaption = "Users"
        };
        public static ScreenAccessProfile User = new ScreenAccessProfile("frm_User", Users) { ScreenCaption = "New user" };
        public static ScreenAccessProfile UserList = new ScreenAccessProfile("frm_UserList", Users)
        {
            ScreenCaption = "Users",
            Actions = new List<Master.Actions>() { Master.Actions.Show, Master.Actions.Open }
        };


        public static ScreenAccessProfile AppSettings = new ScreenAccessProfile("elm_ApplicationSettings")
        {
            Actions = new List<Master.Actions>() { Master.Actions.Show },
            ScreenCaption = "Settings"
        };
        public static ScreenAccessProfile GlobalAppSettings = new ScreenAccessProfile("frm_ApplicationSettings", AppSettings) { ScreenCaption = "Application settings" };
        public static ScreenAccessProfile UserSettingsProfile = new ScreenAccessProfile("frm_UserSettingsProfile", AppSettings) { ScreenCaption = "New user settings profile" };
        public static ScreenAccessProfile UserSettingsProfileList = new ScreenAccessProfile("frm_UserSettingsProfileList", AppSettings) { ScreenCaption = "User settings profiles", Actions = new List<Master.Actions>() { Master.Actions.Show, Master.Actions.Open } };
        public static ScreenAccessProfile UserScreenAccessProfile = new ScreenAccessProfile("frm_UserScreenAccessProfile", AppSettings) { ScreenCaption = "New screen access profile" };
        public static ScreenAccessProfile UserScreenAccessProfileList = new ScreenAccessProfile("frm_UserScreenAccessProfileList", AppSettings) { ScreenCaption = "Screen access profiles", Actions = new List<Master.Actions>() { Master.Actions.Show, Master.Actions.Open } };



        public static List<ScreenAccessProfile> GetScreens
        {
            get
            {
                Type t = typeof(Screens);
                FieldInfo[] fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);
                var list = new List<ScreenAccessProfile>();
                foreach (var item in fields)
                {
                    var obj = item.GetValue(null);
                    if (obj != null && obj.GetType() == typeof(ScreenAccessProfile))
                        list.Add((ScreenAccessProfile)obj);
                }
                return list;
            }
        }
    }
}

