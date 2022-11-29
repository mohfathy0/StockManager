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
    public partial class frm_MasterEntry : frm_Master
    {
        public frm_MasterEntry()
        {
            InitializeComponent();
        }

        public static void ResizeEmptySpace(DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemRight, DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemLeft, DevExpress.XtraLayout.EmptySpaceItem emptySpaceItemTop)
        {
            emptySpaceItemTop.Height = 40;
            if (emptySpaceItemRight != null && emptySpaceItemLeft != null)
            {
                int emptyWidth = emptySpaceItemRight.Width + emptySpaceItemLeft.Width;
                emptySpaceItemRight.Width = emptyWidth / 2;
                emptySpaceItemLeft.Width = emptyWidth / 2;
            }
        }
    }
}
