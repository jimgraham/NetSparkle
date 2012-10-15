using System.Diagnostics;
using AppLimit.NetSparkle.Interfaces;

namespace AppLimit.NetSparkle
{
    /// <summary>
    /// A diagnostic accessor
    /// </summary>
    public class NetSparkleAssemblyDiagnosticsAccessor : INetSparkleAssemblyAccessor
    {
        private readonly string fileVersion;
        private readonly string productVersion;
        private readonly string productName;
        private readonly string companyName;
        private readonly string legalCopyright;
        private readonly string fileDescription;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="assemblyName">the assembly name</param>
        public NetSparkleAssemblyDiagnosticsAccessor(string assemblyName)
        {
            if (assemblyName == null)
            {
                return;
            }
            fileVersion = FileVersionInfo.GetVersionInfo(assemblyName).FileVersion;
            productVersion = FileVersionInfo.GetVersionInfo(assemblyName).ProductVersion;
            productName = FileVersionInfo.GetVersionInfo(assemblyName).ProductName;
            companyName = FileVersionInfo.GetVersionInfo(assemblyName).CompanyName;
            legalCopyright = FileVersionInfo.GetVersionInfo(assemblyName).LegalCopyright;
            fileDescription = FileVersionInfo.GetVersionInfo(assemblyName).FileDescription;
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// Gets the Title
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                return productName;                
            }
        }

        /// <summary>
        /// Gets the version
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return fileVersion;
            }
        }        

        /// <summary>
        /// Gets the description
        /// </summary>
        public string AssemblyDescription
        {
            get { return fileDescription; }
        }

        /// <summary>
        /// gets the product
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                return productVersion;                                
            }
        }

        /// <summary>
        /// Gets the copyright
        /// </summary>
        public string AssemblyCopyright
        {
            get { return legalCopyright; }
        }

        /// <summary>
        /// Gets the company
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                return companyName;                  
            }
        }
        #endregion
    }
}