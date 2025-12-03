using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity
{
    public class FileStructureInfo
    {
        public FileInfo File { get; set; }
        public string Checksum { get => GetChecksum(); private set; } = string.Empty;
        public FileStructureInfo() { }

        private string GetChecksum()
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(File.FullName))
                {
                    return BitConverter
                        .ToString(md5.ComputeHash(stream))
                        .Replace("-", "")
                        .ToLowerInvariant();
                }
            }
        }

        public void CopyTo(string path)
        {
            try
            {
                File.CopyTo(@$"{Environment.GetEnvironmentVariable("windir")}\Atlas" + path);
            } catch (IOException ex)
            {
                App.logger.Error("[INTEGRITY][FileStructureInfo][CopyTo] An error occured when trying to copy a file: \n\t" + ex.Message + "\n\t" + ex.InnerException + "\n\t" + ex.StackTrace);
            }
        }

    }
}
