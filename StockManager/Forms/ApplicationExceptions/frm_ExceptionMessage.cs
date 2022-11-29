using DevExpress.XtraEditors;
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
    public partial class frm_ExceptionMessage : DevExpress.XtraEditors.XtraForm
    {
        Form _frm;
        public frm_ExceptionMessage(string message, string stackTrace, Form frm)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txt_Message.Text = message;
            memoEdit1.Text = stackTrace;
            _frm = frm;
            this.FormClosing += Frm_ExceptionMessage_FormClosing;
        }

        private void Frm_ExceptionMessage_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm_MainForm.Instance.CloseTab(_frm);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}