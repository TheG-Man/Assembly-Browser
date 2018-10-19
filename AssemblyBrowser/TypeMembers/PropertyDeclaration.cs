using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class PropertyDeclaration: ITypeMemberDeclaration
    {
        private readonly Modifiers _modifiers;
        private readonly List<string> _genericParameters;

        public string TypeName { get; }
        public string Name { get; }
        public bool IsGeneric { get; }
        public bool CanRead { get; }
        public bool CanWrite { get; }
        public Modifiers Modifiers => _modifiers;
        public IEnumerable<string> GenericParameters => _genericParameters;

        public PropertyDeclaration(string name, string typeName, bool isGeneric, bool canRead, bool canWrite, PropertyModifiers modifiers, List<string> genericParameters)
        {
            Name = name;
            TypeName = typeName;
            IsGeneric = isGeneric;
            CanRead = canRead;
            CanWrite = canWrite;

            _modifiers = modifiers;
            _genericParameters = genericParameters;
        }
    }
}
