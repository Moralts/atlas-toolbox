using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HourglassGameOnWPF.ViewModel
{
    /// <summary>
    /// This is an integrity checker that will scan the system for key AtlasOS required modules, such as AtlasModules/AtlasDesktop file structure, possibly also some registry entries in the future for more deep analysis.
    ///
    /// Will give detailed explanations of missing/incorrect things, and (maybe) tell the user to install all Patches Toolbox before making changes.
    /// Maybe will provide a fixer for things such as AtlasFolder settings not being properly set.
    /// Will probably require work on services to create an integrity checker for each of them.
    /// </summary>
    public class IntegrityViewModel
    {
        /// <summary>
        /// TODOS:
        /// - Check for AtlasModules folder existence
        ///     - Check the structure of AtlasModules
        /// - Check for AtlasDesktop existence
        ///     - Check the structure of AtlasDesktop
        /// - Check for key registry entries
        ///
        /// - Maybe make services/interface/something else-other to create error items with a name and description
        ///     - Registries should have "expected" and "actual" value
        ///     - Structure errors should be "missing items"
        /// - Should check for patches and ask to apply them before checking for integrity issues
        /// 


        // temporary (for testing purposes. Will have a more custom "user selection based" integrity file verifier in the future) 
        const string PATCH_ATLAS_FOLDER_PATH =  "C:\\Users\\TheyCreeper\\Desktop\\PatchFiles\\AtlasFolder";
        const string PATCH_ATLAS_MODULES_PATH = "C:\\Users\\TheyCreeper\\Desktop\\PatchFiles\\AtlasModules"; 

        // semi-temporary (path should change, value should still exist)
        const string ATLAS_FOLDER_PATH = "C:\\Users\\TheyCreeper\\Desktop\\Windows\\AtlasFolder";
        const string ATLAS_MODULES_PATH = "C:\\Users\\TheyCreeper\\Desktop\\Windows\\AtlasModules";

        public IntegrityViewModel() { }



    }
}
