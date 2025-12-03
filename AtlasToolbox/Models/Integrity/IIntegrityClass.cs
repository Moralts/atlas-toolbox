using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity
{

    public interface IIntegrityClass
    {
        string Name { get; set; }
        string Description { get; set; }
        Severity Severity { get; set; }

        bool TryFix();
    }
}
