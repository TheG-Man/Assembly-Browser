using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AssemblyBrowser.TypeMembers
{
    public class FieldDeclaration: ITypeMemberDeclaration, IGeneralizable
    {
        private readonly Modifiers _modifiers;
        private readonly List<string> _genericParameters;

        public Modifiers Modifiers => _modifiers;
        public IEnumerable<string> GenericParameters => _genericParameters;
        public string TypeName { get; }
        public string Name { get; }
        public bool IsGeneric { get; }

        public FieldDeclaration(string name, string typeName, bool isGeneric, Modifiers modifiers, List<string> genericParameters)
        {
            Name = name;
            TypeName = typeName;
            IsGeneric = isGeneric;

            _modifiers = modifiers;
            _genericParameters = genericParameters;
        }
    }
}
