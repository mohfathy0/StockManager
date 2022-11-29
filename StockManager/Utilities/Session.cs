using DataModel;
using StockManager.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StockManager.Utilities.ScreenAccessProfile;

namespace StockManager.Utilities
{
    public class Session
    {
        private static User _user;
        public static User User { get { return _user; } }

        public static void SetUser(User user)
        {
            _user = user;
            ScreenAccessProfile profile = new ScreenAccessProfile("");
            List<ScreenAccessProfile> profils = new List<ScreenAccessProfile>();
            _screenAccess = profils;
            _user = user;

            using (DatabaseContext ctx = new DatabaseContext())
                _screenAccess = (from s in Screens.GetScreens
                                 from d in ctx.UserAccessProfileDetails
                                 .Where(x => x.UserAccessProfileId == user.ScreenProfileId && x.ScreenId == s.ScreenId).DefaultIfEmpty()
                                 select new ScreenAccessProfile(s.ScreenName)
                                 {
                                     ScreenName = s.ScreenName,
                                     ParentScreenId = s.ParentScreenId,
                                     ScreenCaption = s.ScreenCaption,
                                     ScreenId = s.ScreenId,
                                     Actions = s.Actions,
                                     CanAdd = (d == null ) ? false : d.CanAdd,
                                     CanDelete = (d == null) ? false : d.CanDelete,
                                     CanOpen = (d == null) ? false : d.CanOpen,
                                     CanPrint = (d == null) ? false : d.CanPrint,
                                     CanEdit = (d == null) ? false : d.CanEdit,
                                     CanShow = (d == null) ? false : d.CanShow,
                                 }).ToList();
        }


        private static List<Utilities.ScreenAccessProfile> _screenAccess;
        public static List<Utilities.ScreenAccessProfile> ScreenAccess
        {
            get
            {
                if (User.UserType == (byte)Master.UserType.Admin)
                { 
                    return Screens.GetScreens;
                }
                return _screenAccess;
            }
        }
        public static void RefreshGlobalSettings()
        {
            _globalSettings = null; _global = null;

            Global.GetType().GetProperties().Initialize();
            globalSettings.GetType().GetProperties().Initialize();
        }
        private static BindingList<GlobalSetting> _globalSettings;
        public static BindingList<GlobalSetting> globalSettings
        {
            get
            {
                using (DatabaseContext ctx = new DatabaseContext())
                {

                    if (_globalSettings == null || _globalSettings.Count == 0)
                        _globalSettings = ctx.GlobalSettings.ToBindingList();
                }
                return _globalSettings;
            }
        }
        private static GlobalSettingsTemplate _global;
        public static GlobalSettingsTemplate Global
        {
            get
            {
                if (_global == null)
                {
                    _global = new GlobalSettingsTemplate();
                }
                return _global;
            }
        }
        public static UserSettingsTemplate _userSettings;
        public static UserSettingsTemplate userSettings
        {
            get
            {
                if (_userSettings == null)
                    _userSettings = new UserSettingsTemplate(User.SettingsProfileId);
                return _userSettings;
            }
        }
  
    }
}
