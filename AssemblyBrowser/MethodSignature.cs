using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    internal class MethodSignature
    {
        internal string AccessLevel { get; }
        internal List<string> Modifiers { get; }
        internal string ReturnValue { get; }
        internal string MethodName { get; }
        internal List<string> Parameters { get; }

        internal MethodSignature(string accessLevel, object[] modifiers, string returnValue, string methodName, object[] parameters)
        {
            AccessLevel = accessLevel;

            Modifiers = new List<string>();
            Modifiers.AddRange((IEnumerable<string>)modifiers);

            ReturnValue = returnValue;
            MethodName = methodName;

            Parameters = new List<string>();
            Parameters.AddRange((IEnumerable<string>)parameters);
        }
    }
}
