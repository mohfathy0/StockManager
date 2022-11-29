using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class GlobalSetting
    {
        [Key]
        public int Id { get; set; }
        public string SettingName { get; set; }
        public byte[] SettingValue { get; set; }
    }
}
