using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.ViewModel
{
    class TypeViewModel
    {
        private readonly TypeDeclaration _typeDeclaration;

        public string StringRepresentation => GetStringRepresentation();
        public IEnumerable<object> Members
        {
            get
            {
                foreach (FieldDeclaration fieldDeclaration in _typeDeclaration.Fields)
                {
                    yield return new FieldViewModel(fieldDeclaration);
                }
                foreach (PropertyDeclaration propertyDeclaration in _typeDeclaration.Properties)
                {
                    yield return new PropertyViewModel(propertyDeclaration);
                }
                foreach (MethodDeclaration methodDeclaration in _typeDeclaration.Methods)
                {
                    yield return new MethodViewModel(methodDeclaration);
                }
                foreach (EventDeclaration eventDeclaration in _typeDeclaration.Events)
                {
                    yield return new EventViewModel(eventDeclaration);
                }
                foreach (TypeDeclaration typeDeclaration in _typeDeclaration.NestedTypes)
                {
                    yield return new TypeViewModel(typeDeclaration);
                }
            }
        }

        public TypeViewModel(TypeDeclaration typeDeclaration)
        {
            _typeDeclaration = typeDeclaration;
        }

        private string GetStringRepresentation()
        {
            string stringRepresentation;
            string classOrInterfaceKeyword = _typeDeclaration.IsInterface ? "interface" : "class";

            stringRepresentation = string.Format("{0} {1} {2}", GetModifiers(_typeDeclaration.Modifiers.CSharpModifiers), classOrInterfaceKeyword, _typeDeclaration.Name);

            if (_typeDeclaration.IsGeneric)
            {
                stringRepresentation += string.Format("<{0}>", GetModifiers(_typeDeclaration.GenericParameters));
            }

            if (_typeDeclaration.BaseType != null)
                stringRepresentation += string.Format(" : {0} ", _typeDeclaration.BaseType.Split('.').Last());

            if (_typeDeclaration.ImplementedInterfaces.Count() > 0)
            {
                foreach (string interfaceName in _typeDeclaration.ImplementedInterfaces)
                {
                    stringRepresentation += string.Format("{0} ", interfaceName);
                }
            }

            return stringRepresentation;
        }

        public string GetModifiers(IEnumerable<string> modifiers)
        {
            string modifiersLine = string.Empty;

            foreach (string modifier in modifiers)
            {
                modifiersLine += string.Format("{0} ", modifier);
            }

            return modifiersLine.TrimEnd();
        }
    }
}
