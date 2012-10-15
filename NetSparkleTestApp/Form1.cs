using System;
using System.Windows.Forms;
using AppLimit.NetSparkle;

namespace NetSparkleTestApp
{
    public partial class Form1 : Form
    {
        private readonly Sparkle _sparkle;

        /// <summary>
        /// Form to show update available
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            _sparkle = new Sparkle("https://update.applimit.com/netsparkle/versioninfo.xml")
            {
                ShowDiagnosticWindow = true,
                TrustEverySSLConnection = true,
                //EnableSystemProfiling = true,
                //SystemProfileUrl = new Uri("http://update.applimit.com/netsparkle/stat/profileInfo.php")
            };

            _sparkle.UpdateDetected += new UpdateDetected(_sparkle_updateDetected);
            //_sparkle.EnableSilentMode = true;
            //_sparkle.HideReleaseNotes = true;

            _sparkle.StartLoop(true);
        }

        static void _sparkle_updateDetected(object sender, UpdateDetectedEventArgs e)
        {
            DialogResult res = MessageBox.Show("Update detected, perform unattended", "Update", MessageBoxButtons.YesNoCancel);

            switch (res)
            {
                case DialogResult.Yes:
                    e.NextAction = NextUpdateAction.PerformUpdateUnattended;
                    break;
                case DialogResult.Cancel:
                    e.NextAction = NextUpdateAction.ProhibitUpdate;
                    break;
                default:
                    e.NextAction = NextUpdateAction.ShowStandardUserInterface;
                    break;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _sparkle.StopLoop();
        }

        private void btnStopLoop_Click(object sender, EventArgs e)
        {
            _sparkle.StopLoop();
        }

        private void btnTestLoop_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_sparkle.IsUpdateLoopRunning ? "Loop is running" : "Loop is not running");
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            
        }
    }
}
