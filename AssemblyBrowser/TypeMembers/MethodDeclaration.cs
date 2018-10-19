using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class MethodDeclaration: ITypeMemberDeclaration, IGeneric
    {
        private readonly Modifiers _modifiers;
        private readonly List<ParameterDeclaration> _parameters;
        private readonly List<string> _genericParameters;

        public Modifiers Modifiers => _modifiers;
        public IEnumerable<ParameterDeclaration> Parameters => _parameters;
        public IEnumerable<string> GenericParameters => _genericParameters;

        public string ReturnTypeName { get; }
        public string Name { get; }
        public bool IsConstructor { get; }
        public bool IsGeneric { get; }
        public bool IsExtention { get; }

        public MethodDeclaration(string name, string returnTypeName, bool isConstructor, bool isGeneric, bool isExtention,
            Modifiers modifiers, List<ParameterDeclaration> parameters, List<string> genericParameters)
        {
            Name = name;
            ReturnTypeName = returnTypeName;
            IsConstructor = isConstructor;
            IsGeneric = isGeneric;
            IsExtention = isExtention;

            _modifiers = modifiers;
            _parameters = parameters;
            _genericParameters = genericParameters;
        }
    }
}
