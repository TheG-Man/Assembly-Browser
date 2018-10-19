using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class EventDeclaration : ITypeMemberDeclaration
    {
        private readonly Modifiers _modifiers;

        public Modifiers Modifiers => _modifiers;
        public string TypeName { get; }
        public string Name { get; }

        public EventDeclaration(string name, string typeName, Modifiers modifiers)
        {
            Name = name;
            TypeName = typeName;

            _modifiers = modifiers;
        }
    }
}
