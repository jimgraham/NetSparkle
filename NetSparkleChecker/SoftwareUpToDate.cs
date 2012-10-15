using System;
using System.Windows.Forms;

namespace NetSparkleChecker
{
    public partial class SoftwareUpToDate : Form
    {
        public SoftwareUpToDate()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
