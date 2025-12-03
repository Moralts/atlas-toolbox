using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity.IntegrityCheckers
{
    public class FileStructureIntegrity : IIntegrityClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public FileStrucSeverity fileSeverity { get; set; }
        public FileStructureInfo Actual { get; set; }
        public FileStructureInfo Expected { get; set; }

        public FileStructureIntegrity() { }

        public FileStructureIntegrity(string name, string description, FileStrucSeverity fileStrucSeverity, FileInfo actual, FileInfo expected)
        {
            Name = name;
            Description = description;
            switch (fileSeverity)
            {
                default:
                    Description = "Value was not expected";
                    Severity = Severity.Information;
                    break;
                case FileStrucSeverity.Missing:
                    Description = "File was not found. This will cause issues.";
                    Severity = Severity.Critical;
                    break;
                case FileStrucSeverity.Modified:
                    Description = "File was modified. May cause issues";
                    Severity = Severity.Information;
                    break;
                case FileStrucSeverity.Moved:
                    Description = "File was moved. Will probably cause issues";
                    Severity = Severity.Warning;
                    break;
            }
            fileSeverity = fileStrucSeverity;
            Actual = new FileStructureInfo{ File = actual };
            Expected = new FileStructureInfo { File = expected }; ;
        }


        /// <summary>
        /// Tries to fix the file structure
        /// </summary>
        /// <returns></returns>
        public bool TryFix()
        {
            string key = "Atlas";
            int index = Expected.File.FullName.IndexOf(key);

            // Gets the path for the new file to be copied
            string simplePath = index >= 0
                ? Expected.File.FullName.Substring(index + key.Length)
                : string.Empty;

            try
            {
                Expected.CopyTo(simplePath.Split(Expected.File.Name)[0]);
                if (fileSeverity == FileStrucSeverity.Moved)
                {
                    Actual.File.Delete();
                    return !VerifyFix(Expected, Actual).Any(x => x == false);
                }
                return !VerifyFix(Expected).Any(x => x == false);

            } catch (IOException ex)
            {
                App.logger.Error("[INTEGRITY][FileStructure] An error occured when trying to fix file structure integrity issues: \n\t" + ex.Message + "\n\t" + ex.InnerException + "\n\t" + ex.StackTrace);
            }
            return false;
        }

        public bool[] VerifyFix(FileStructureInfo expected, FileStructureInfo actual = null)
        {
            // Get the value of the file on the actual path
            FileStructureInfo actualExpectedFile = new FileStructureInfo
            { 
                File = new FileInfo(expected.File.FullName)
            };

            if (actualExpectedFile is null)
            {
                return new bool[]
                {
                    actualExpectedFile.File.Exists,
                    actualExpectedFile.File.FullName == expected.File.FullName,
                    actualExpectedFile.Checksum == expected.Checksum,
                };
            }
            else
            {
                return new bool[]
                {
                    actualExpectedFile.File.Exists,
                    actualExpectedFile.File.FullName == expected.File.FullName,
                    actualExpectedFile.Checksum == expected.Checksum,
                    !actual.File.Exists,
                };
            }
        }
    }
}
