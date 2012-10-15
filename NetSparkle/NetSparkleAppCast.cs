using System;
using System.IO;
using System.Xml;
using System.Net;

namespace AppLimit.NetSparkle
{
    /// <summary>
    /// An app-cast 
    /// </summary>
    public class NetSparkleAppCast
    {
        private readonly NetSparkleConfiguration _config;
        private readonly String _castUrl;

        private const String itemNode = "item";
        private const String enclosureNode = "enclosure";
        private const String releaseNotesLinkNode = "sparkle:releaseNotesLink";
        private const String versionAttribute = "sparkle:version";
        private const String dasSignature = "sparkle:dsaSignature";
        private const String urlAttribute = "url";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="castUrl">the URL of the appcast file</param>
        /// <param name="config">the current configuration</param>
        public NetSparkleAppCast(string castUrl, NetSparkleConfiguration config)
        {
            _config     = config;
            _castUrl    = castUrl;
        }

        /// <summary>
        /// Gets the latest version
        /// </summary>
        /// <returns>the AppCast item corresponding to the latest version</returns>
        public NetSparkleAppCastItem GetLatestVersion()
        {
            NetSparkleAppCastItem latestVersion = null;
          
            // build a http web request stream
            WebRequest request = WebRequest.Create(_castUrl);
            request.UseDefaultCredentials = true;

            // request the cast and build the stream
            WebResponse response = request.GetResponse();
            using (Stream inputstream = response.GetResponseStream())
            {
                NetSparkleAppCastItem currentItem = null;
                if (inputstream == null)
                {
                    return null;
                }
                using (XmlTextReader reader = new XmlTextReader(inputstream))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            switch (reader.Name)
                            {
                                case itemNode:
                                    {
                                        currentItem = new NetSparkleAppCastItem();
                                        break;
                                    }
                                case releaseNotesLinkNode:
                                    {
                                        if (currentItem != null)
                                        {
                                            currentItem.ReleaseNotesLink = reader.ReadString();
                                            currentItem.ReleaseNotesLink = currentItem.ReleaseNotesLink.Trim('\n');
                                        }
                                        break;
                                    }
                                case enclosureNode:
                                    {
                                        if (currentItem != null)
                                        {
                                            currentItem.Version = reader.GetAttribute(versionAttribute);
                                            currentItem.DownloadLink = reader.GetAttribute(urlAttribute);
                                            currentItem.DSASignature = reader.GetAttribute(dasSignature);
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (reader.NodeType == XmlNodeType.EndElement)
                        {
                            switch (reader.Name)
                            {
                                case itemNode:
                                    {
                                        if (latestVersion == null)
                                            latestVersion = currentItem;
                                        else if (currentItem.CompareTo(latestVersion) > 0)
                                        {
                                            latestVersion = currentItem;
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }
            }

            // add some other attributes
            if (latestVersion != null)
            {
                latestVersion.AppName = _config.ApplicationName;
                latestVersion.AppVersionInstalled = _config.InstalledVersion;
            }
            // go ahead
            return latestVersion;
        }
    }
}
