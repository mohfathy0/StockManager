using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public partial class UserSettingsProfileProperty
    {
        [Key]
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public string PropertyName { get; set; }
        public byte[] PropertyValue { get; set; }
    }
}
