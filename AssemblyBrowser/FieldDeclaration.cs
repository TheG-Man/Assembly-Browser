using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    internal class FieldDeclaration: IClassMemberDeclaration
    {
        public List<string> Attributes { get; }
        public List<string> Modifiers { get; }
        public string Type { get; }
        public string Name { get; }

        internal FieldDeclaration(object[] attributes, object[] modifiers, string fieldType, string fieldName)
        {
            Attributes = new List<string>();
            Attributes.AddRange((IEnumerable<string>)attributes);

            Modifiers = new List<string>();
            Modifiers.AddRange((IEnumerable<string>)modifiers);

            Type = fieldType;
            Name = fieldName;
        }
    }
}
