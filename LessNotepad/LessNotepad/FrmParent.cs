using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LessNotepad
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void ToolStripMenuItemNew_Click(object sender, EventArgs e)
        {
            FirmChild child = new FirmChild();
            child.MdiParent = this;
            child.Show();
        }

        private void ToolStripMenuItemClose_Click(object sender, EventArgs e)
        {
            Form frm = this.ActiveMdiChild;
            frm.Close();
        }

        private void ToolStripMenuItemCloseAll_Click(object sender, EventArgs e)
        {
            foreach(Form form in this.MdiChildren)
            {
                Form frm = this.ActiveMdiChild;
                frm.Close();
            }
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
