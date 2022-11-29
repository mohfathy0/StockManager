using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockManager.Utilities
{
    public class GlobalSettingsTemplate
    {
        public GlobalGeneralSettings General { get; set; }
        public GlobalSettingsTemplate()
        {
            General = new GlobalGeneralSettings();
        }
        public static BaseEdit GetPropertyControl(string propName, object propertyValue)
        {
            BaseEdit edit = null;
            switch (propName)
            {
                case nameof(GlobalSettingsTemplate.General.DefaultCurrencyNumberDecimalDigits):
                    edit = new SpinEdit();
                    ((SpinEdit)edit).InitializeNumber();
                    break;
                case nameof(GlobalSettingsTemplate.General.DefaultCurrencyCultureInfo):
                    edit = new TextEdit();
                    break;
            }
            return edit;
        }
        public class GlobalGeneralSettings
        {
            //API Key = aabad6fe5e7ce9ee40aa
            public string CurrencyExchangeApiKey { get { return DataMaster.FromByteArray<string>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            // pl
            public string DefaultCurrencyCultureInfo { get { return DataMaster.FromByteArray<string>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            // .
            public string DefaultCurrencyDecimalSeperator { get { return DataMaster.FromByteArray<string>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            // ,
            public string DefaultCurrencyGroupSeparator { get { return DataMaster.FromByteArray<string>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            // 2
            public int? DefaultCurrencyNumberDecimalDigits { get { return DataMaster.FromByteArray<int?>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            public int? DefaultDueDateHighlighter { get { return DataMaster.FromByteArray<int?>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }
            public string DefaultCurrencyISO { get { return DataMaster.FromByteArray<string>(DataMaster.GetGlobalSettingValue(DataMaster.GetCallerName())); } }

        }
    }
}
