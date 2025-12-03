using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity.FixFailures
{
    public interface IFixFailure
    {
        string Name { get; }
        string Description { get; }
        Severity Severity { get; }
        object FixType { get; }
    }
}
