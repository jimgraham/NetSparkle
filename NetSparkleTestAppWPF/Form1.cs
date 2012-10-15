using System;
using System.Windows.Forms;
using AppLimit.NetSparkle;

namespace NetSparkleTestAppWPF
{
    public partial class Form1 : Form
    {
        private readonly Sparkle _sparkle = new Sparkle("http://update.applimit.com/netsparkle/versioninfo.xml"); 

        public Form1()
        {
            InitializeComponent();

            // remove the netsparkle key from registry 
            try
            {
                Microsoft.Win32.Registry.CurrentUser.DeleteSubKeyTree("Software\\Microsoft\\NetSparkleTestApp");
            }
            catch { }

            _sparkle.StartLoop(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _sparkle.StopLoop();
            Close();
        }
    }
}
