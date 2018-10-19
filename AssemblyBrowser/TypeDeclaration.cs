using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser
{
    public class TypeDeclaration: IGeneric
    {
        private readonly Modifiers _modifiers;
        private readonly List<string> _implementedInterfaces;
        private readonly List<string> _genericParameters;
        private readonly List<FieldDeclaration> _fields;
        private readonly List<PropertyDeclaration> _properties;
        private readonly List<MethodDeclaration> _methods;
        private readonly List<EventDeclaration> _events;
        private readonly List<TypeDeclaration> _nestedTypes;

        public string Name { get; }
        public string FullName { get; }
        public string BaseType { get; }
        public bool IsInterface { get; }
        public bool IsGeneric { get; }

        public Modifiers Modifiers => _modifiers;
        public IEnumerable<string> ImplementedInterfaces => _implementedInterfaces;
        public IEnumerable<string> GenericParameters => _genericParameters;
        public IEnumerable<FieldDeclaration> Fields => _fields;
        public IEnumerable<PropertyDeclaration> Properties => _properties;
        public IEnumerable<MethodDeclaration> Methods => _methods;
        public IEnumerable<EventDeclaration> Events => _events;
        public IEnumerable<ITypeMemberDeclaration> Members {
            get
            {
                IEnumerable<ITypeMemberDeclaration> fields = new List<ITypeMemberDeclaration>(_fields);
                IEnumerable<ITypeMemberDeclaration> properties = new List<ITypeMemberDeclaration>(_properties);
                IEnumerable<ITypeMemberDeclaration> methods = new List<ITypeMemberDeclaration>(_methods);
                IEnumerable<ITypeMemberDeclaration> events = new List<ITypeMemberDeclaration>(_events);
                return fields.Concat(properties).Concat(methods).Concat(events);
            }
        }

        public IEnumerable<TypeDeclaration> NestedTypes => _nestedTypes;

        public TypeDeclaration(string name, string fullName, string baseType, bool isInterface, bool isGeneric, 
            Modifiers modifiers, List<string> implementedInterfaces, List<string> genericParameters,
            List<FieldDeclaration> fields, List<PropertyDeclaration> properties, List<MethodDeclaration> methods, 
            List<EventDeclaration> events, List<TypeDeclaration> nestedTypes)
        {
            Name = name;
            FullName = fullName;
            BaseType = baseType;
            IsInterface = isInterface;
            IsGeneric = isGeneric;

            _modifiers = modifiers;
            _implementedInterfaces = implementedInterfaces;
            _genericParameters = genericParameters;
            _fields = fields;
            _properties = properties;
            _methods = methods;
            _events = events;
            _nestedTypes = nestedTypes;
        }
    }
}
