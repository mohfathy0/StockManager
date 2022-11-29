using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ExceptionLog
    {
        [Key]
        public int Id { get; set; }
        public System.DateTime logDate { get; set; }
        [StringLength(50)]
        public string logThread { get; set; }
        [StringLength(50)]
        public string logLevel { get; set; }
        [StringLength(50)]
        public string ActionType { get; set; }
        [StringLength(50)]
        public string ScreenName { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        public Nullable<int> UserId { get; set; }
        [StringLength(50)]
        public string RecordName { get; set; }
        public Nullable<int> RecordId { get; set; }
        [StringLength(50)]
        public string MethodName { get; set; }
        [StringLength(255)]
        public string ClassName { get; set; }
        public Nullable<int> LineNumber { get; set; }
        [StringLength(300)]
        public string logSource { get; set; }
        [StringLength(4000)]
        public string logMessage { get; set; }
        [StringLength(4000)]
        public string exception { get; set; }
    }
}
