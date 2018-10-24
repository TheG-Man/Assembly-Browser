using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class ParameterDeclaration : ITypeMemberDeclaration, IGeneric
    {
        private readonly Modifiers _modifiers;
        private readonly List<string> _genericParameters;

        public Modifiers Modifiers => _modifiers;
        public IEnumerable<string> GenericParameters => _genericParameters;
        public string TypeName { get; }
        public string Name { get; }
        public bool IsGeneric { get; }
        public bool IsClass { get; }

        public ParameterDeclaration(string name, string typeName, bool isGeneric, bool isClass, Modifiers modifiers, List<string> genericParameters)
        {
            Name = name;
            TypeName = typeName;
            IsGeneric = isGeneric;
            IsClass = isClass;

            _modifiers = modifiers;
            _genericParameters = genericParameters;
        }
    }
}
