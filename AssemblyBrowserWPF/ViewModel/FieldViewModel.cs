using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.ViewModel
{
    class FieldViewModel
    {
        private readonly FieldDeclaration _fieldDeclaration;

        public string StringRepresentation => GetStringRepresentation();

        public FieldViewModel(FieldDeclaration fieldDeclaration)
        {
            _fieldDeclaration = fieldDeclaration;
        }

        private string GetStringRepresentation()
        {
            string typeName = _fieldDeclaration.TypeName;

            if (_fieldDeclaration.IsGeneric)
            {
                typeName += string.Format("<{0}>", GetModifiers(_fieldDeclaration.GenericParameters));
            }

            return string.Format("{0} {1} {2}", GetModifiers(_fieldDeclaration.Modifiers.CSharpModifiers), typeName, _fieldDeclaration.Name); ;
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
