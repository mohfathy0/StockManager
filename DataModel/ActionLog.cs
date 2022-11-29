using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ActionLog
    {
        [Key]
        public long Id { get; set; }
        public int UserId { get; set; }
        public DateTime ActionDate { get; set; }
        public int ScreenId { get; set; }
        public byte ActionType { get; set; }
        public int RecordId { get; set; }
        public string RecordName { get; set; }
    }
}
