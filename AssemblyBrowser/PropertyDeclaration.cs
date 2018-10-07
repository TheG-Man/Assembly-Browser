using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    internal class PropertyDeclaration: IClassMemberDeclaration
    {
        public List<string> Attributes { get; }
        public List<string> Modifiers { get; }
        public string Type { get; }
        public string Name { get; }

        internal PropertyDeclaration(object[] attributes, object[] modifiers, string propertyType, string propertyName)
        {
            Attributes = new List<string>();
            Attributes.AddRange((IEnumerable<string>)attributes);

            Modifiers = new List<string>();
            Modifiers.AddRange((IEnumerable<string>)modifiers);

            Type = propertyType;
            Name = propertyName;
        }
    }
}
