using AtlasToolbox.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtlasToolbox.Models.Integrity.IntegrityCheckers
{
    public class RegistryIntegrity : IIntegrityClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Severity Severity { get; set; }
        public RegistrySeverity RegistrySeverity { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public object Actual { get; set; }
        public object Expected { get; set; }

        public RegistryIntegrity() { }

        /// <summary>
        /// Big regitry integrity. Is used when displaying small-ish amount of info to a user.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="severity"></param>
        public RegistryIntegrity(string key, string value, string actual, string expected, RegistrySeverity registrySeverity) 
        {
            Name = "Registry value at: " + key + value;
            switch (registrySeverity)
            {
                default:
                    Description = "Value was not expected";
                    Severity = Severity.Information;
                    break;
                case RegistrySeverity.ValueMissing:
                    Description = "Value was not found. may cause issues.";
                    Severity = Severity.Warning;
                    break;
                case RegistrySeverity.ValueIsWrong:
                    Description = "Value was not expected, but can be ignored";
                    Severity = Severity.Information;
                    break;
                case RegistrySeverity.KeyMissing:
                    Description = "Key was not found. High chances of errors";
                    Severity = Severity.Critical;
                    break;
            }
            RegistrySeverity = registrySeverity;
            Key = key;
            Value = value;
            Actual = actual;
            Expected = expected;
        }

        /// <summary>
        /// Tiny registry integrity, meant to be used when bigger fixes/verifications are done
        /// Mainly used when cheking for playbook installation integrity
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expected"></param>
        public RegistryIntegrity(string key, string value, string expected)
        {
            Key = key;
            Value = value;
            Expected = expected;
        }

        /// <summary>
        /// Tries to fix the registry integrity value
        /// </summary>
        /// <returns></returns>
        public bool TryFix()
        {
            try
            {
                RegistryHelper.SetValue(Key, Value, Expected);

                return RegistryHelper.IsMatch(Key, Value, Expected);
            } catch (Exception ex)
            {
                App.logger.Error("[INTEGRITY][Registry] An error occured when trying to fix integrity issues: \n\t" + ex.Message + "\n\t" + ex.InnerException + "\n\t" + ex.StackTrace);
            }
            return false;
        }
    }
}
