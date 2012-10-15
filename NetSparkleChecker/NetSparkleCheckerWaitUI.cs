﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

using AppLimit.NetSparkle;

namespace NetSparkleChecker
{
    public partial class NetSparkleCheckerWaitUI : Form
    {
        private readonly Sparkle _sparkle;
        private NetSparkleAppCastItem latestVersion;

        public Boolean SparkleRequestedUpdate = false;
        
        public NetSparkleCheckerWaitUI()
        {
            // init ui
            InitializeComponent();

            // get cmdline args
            String[] args = Environment.GetCommandLineArgs();

#if DEBUG
            // enable dialog
            Boolean bShowDiagnosticWindow = true;
#else
            // disable dialog
            Boolean bShowDiagnosticWindow = false;
#endif
            // init sparkle
            _sparkle = new Sparkle(args[2], args[1], bShowDiagnosticWindow);
            
            // set labels
            lblRefFileName.Text = args[1];
            lblRefUrl.Text = args[2];

            if (_sparkle.ApplicationIcon != null)
                imgAppIcon.Image = _sparkle.ApplicationIcon;

            if (_sparkle.ApplicationWindowIcon != null)
                Icon = _sparkle.ApplicationWindowIcon;            

            bckWorker.RunWorkerAsync();
        }

        public void ShowUpdateUI()
        {
            _sparkle.ShowUpdateNeededUI(latestVersion);
        }

        private void bckWorker_DoWork(object sender, DoWorkEventArgs e)
        {            
            // get the config
            NetSparkleConfiguration config = _sparkle.GetApplicationConfig();

            // check for updats
            NetSparkleAppCastItem latestVersion;
            Boolean bUpdateRequired = _sparkle.IsUpdateRequired(config, out latestVersion);
                                
            // save the result
            SparkleRequestedUpdate = bUpdateRequired;
            this.latestVersion = latestVersion;
        }

        private void bckWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // close the form
            Close();
        }        
    }
}
