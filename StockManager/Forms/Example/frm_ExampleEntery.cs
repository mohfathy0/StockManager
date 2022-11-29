using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager.Forms
{
    public partial class frm_ExampleEntery : frm_MasterEntry
    {
        public frm_ExampleEntery()
        {
            InitializeComponent();
        }
        public override void Save()
        {
            throw new OutOfMemoryException();
            base.Save();
        }

    }
}
