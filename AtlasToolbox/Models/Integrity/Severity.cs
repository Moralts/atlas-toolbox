using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity
{
    public enum Severity
    {
        Warning,
        Information,
        Critical,
    }

    public enum RegistrySeverity
    {
        KeyMissing,
        ValueMissing,
        ValueIsWrong,
    }

    // FileStructure-specific
    public enum FileStrucSeverity
    {
        Missing,
        Modified,
        Moved,
    }
}
