using System;
using System.Windows.Forms;

namespace AppLimit.NetSparkle
{
    /// <summary>
    /// Form to show the main window
    /// </summary>
    public partial class NetSparkleMainWindows : Form, IDisposable
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public NetSparkleMainWindows()
        {
            // init ui
            InitializeComponent();
        }

        /// <summary>
        /// Reports a message
        /// </summary>
        /// <param name="message">the message</param>
        public void Report(String message)
        {
            if (lstActions.InvokeRequired)
                lstActions.Invoke(new Action<String>(Report), message);
            else
            {
                // build the message 
                DateTime c = DateTime.Now;
                String msg = "[" + c.ToLongTimeString() + "." + c.Millisecond + "] " + message;

                // report the message into ui
                lstActions.Items.Add(msg);
            }
        }
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            // close the base
            Dispose();
        }
        #endregion
    }
}
