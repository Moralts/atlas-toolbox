using AtlasToolbox.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace AtlasToolbox.Utils
{
    /// <summary>
    /// Patches are small scripts that fix or improve certain functionalities of AtlasOS.
    /// They're meant to be self-contained and run with one powershell script. 
    /// This helper class manages the checking, downloading, and applying of these patches.
    /// 
    /// Patches can be downloaded and applied manually if the user decides, however it is much easier and efficient to use Toolbox
    /// to install them automatically.
    /// 
    /// 
    /// These will possibly be applied to the main AtlasOS playbook as they're published, or the user may need to use Toolbox to apply them 
    /// after applying the playbook.
    /// </summary>
    public class AtlasPatcherHelper
    {
        // Temporary github URL for testing purposes.
        const string RELEASE_URL = "https://api.github.com/repos/TheyCreeper/Patches/releases";

        // static vars & const vars
        static readonly string downloadPath = $@"{Environment.GetEnvironmentVariable("windir")}\AtlasModules\Patches\";
        const string APPLY_PATCH_COMMAND = "powershell -ExecutionPolicy Bypass -File ";
        public static string commandUpdate;

        public static List<Patch> availablePatches = new List<Patch>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool CheckForPatches()
        {
            try
            {
                // get the api result
                string htmlContent = CommandPromptHelper.ReturnRunCommand("curl " + RELEASE_URL);
                var result = JsonDocument.Parse(htmlContent);
                
                if (result.RootElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement element in result.RootElement.EnumerateArray())
                    {
                        string tagName = element.GetProperty("tag_name").GetString();
                    }
                }
            }
            catch (Exception ex)
            {
                App.logger.Error("[UPDATEHELPER] An error occurred while checking for updates.", ex.Message + ex.InnerException);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Download patches with url & curl
        /// </summary>
        /// <param name="downloadUrl"></param>
        public static void DownloadPatch(string downloadUrl)
        {
            try
            {
                string command = $"curl -L {downloadUrl} -o {downloadPath}";
                CommandPromptHelper.RunCommand(command);
                App.logger.Info("[UPDATEHELPER] Patch downloaded successfully.");
            }
            catch (Exception ex)
            {
                App.logger.Error("[UPDATEHELPER] An error occurred while downloading the patch.", ex.Message + ex.InnerException);
            }
        }
        public static void ApplyAllPatches()
        {
            
        }

        public static void ApplyPatch(string patchFilePath)
        {
            try
            {
                CommandPromptHelper.RunCommand(APPLY_PATCH_COMMAND + patchFilePath);
                App.logger.Info("[UPDATEHELPER] Patch applied successfully.");
            }
            catch (Exception ex)
            {
                App.logger.Error("[UPDATEHELPER] An error occurred while applying the patch.", ex.Message + ex.InnerException);
            }
        }
        /// <summary>
        /// Cleans up the download directory after applying patches
        /// </summary>
        public static void CleanupPatches()
        {

        }

        /// <summary>
        /// Returns all applied patches from the registry
        /// </summary>
        public static void ReturnAppliedPatches()
        {
            
        }
    }
}
