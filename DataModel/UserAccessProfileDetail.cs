using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public partial class UserAccessProfileDetail
    {
        [Key]
        public int Id { get; set; }
        public int UserAccessProfileId { get; set; }
        public int ScreenId { get; set; }
        public bool CanShow { get; set; }
        public bool CanOpen { get; set; }
        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanPrint { get; set; }
    }
}
