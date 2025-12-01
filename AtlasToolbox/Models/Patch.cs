using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models
{
    public class Patch
    {
        public string Version { get; set; }
        public string DownloadUrl { get; set; }
        public string InstallDate { get; set; }
        public string Checksum { get; set; }

        public override string ToString()
        {
            return $"{Version ?? "N/A"} - Installed on: {InstallDate ?? "N/A"}";
        }

        public Patch(string version, string downloadUrl, string checksum, string installDate)
        {
            Version = version;
            DownloadUrl = downloadUrl;
            Checksum = checksum;
            InstallDate = installDate;
        }

        public Patch(string version, string downloadUrl, string checksum)
        {
            Version = version;
            DownloadUrl = downloadUrl;
            Checksum = checksum;
            InstallDate = "Not Installed";
        }
    }
}
