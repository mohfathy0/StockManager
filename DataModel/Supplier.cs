using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string CompanyId { get; set; }
        public string SupplierName{ get; set; }
        public string SupplierAddress{ get; set; }
        public string BankName{ get; set; }
        public string BankAddress{ get; set; }
        public string Iban{ get; set; }
        public string Swift{ get; set; }
    }
}
