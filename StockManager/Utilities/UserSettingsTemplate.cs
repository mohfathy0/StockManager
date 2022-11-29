using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Utilities
{
    public class UserSettingsTemplate
    {
        private int ProfileId { get; set; }
        public GeneralSettings General { get; set; }
        public SalesSettings Sales { get; set; }

        public UserSettingsTemplate(int profileId)
        {
            ProfileId = profileId;
            General = new GeneralSettings(profileId);
            Sales = new SalesSettings(profileId);
        }
        public static BaseEdit GetPropertyControl(string propName, object propertyValue)
        {
            BaseEdit edit = null;
            switch (propName)
            {
                case nameof(UserSettingsTemplate.General.MinimumBackwardSearchInDays):
                    edit = new SpinEdit();
                    ((SpinEdit)edit).InitializeNumber();
                    break;
                case nameof(UserSettingsTemplate.Sales.MainCurrencyCode):
                    edit = new TextEdit();
                    break;
                default:
                    break;
            }
            if (edit != null)
            {
                edit.Properties.NullText = string.Empty;
                edit.Name = propName;
                edit.EditValue = propertyValue;
            }
            return edit;
        }
    }
    public class GeneralSettings
    {
        private int ProfileId { get; set; }
        public GeneralSettings(int profileId)
        {
            ProfileId = profileId;
        }
        public int MinimumBackwardSearchInDays { get { return DataMaster.FromByteArray<int>(DataMaster.GetPropertyValue(DataMaster.GetCallerName(), ProfileId)); } }
    }

    public class SalesSettings
    {
        private int ProfileId { get; set; }
        public SalesSettings(int profileId)
        {
            ProfileId = profileId;
        }
        public string MainCurrencyCode { get { return DataMaster.FromByteArray<string>(DataMaster.GetPropertyValue(DataMaster.GetCallerName(), ProfileId)); } }
    }
}