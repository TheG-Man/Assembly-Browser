using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.ViewModel
{
    class PropertyViewModel
    {
        private readonly PropertyDeclaration _propertyDeclaration;

        public string StringRepresentation => GetStringRepresentation();

        public PropertyViewModel(PropertyDeclaration propertyDeclaration)
        {
            _propertyDeclaration = propertyDeclaration;
        }

        private string GetStringRepresentation()
        {
            string modifiers = GetModifiers(_propertyDeclaration.Modifiers.CSharpModifiers);
            string typeName = _propertyDeclaration.TypeName;
            string name = _propertyDeclaration.Name;
            string setterAndGetter = GetSetterAndGetter(_propertyDeclaration);

            if (_propertyDeclaration.IsGeneric)
            {
                typeName += string.Format("<{0}>", GetModifiers(_propertyDeclaration.GenericParameters));
            }

            return string.Format("{0} {1} {2} {3}", modifiers, typeName, name, setterAndGetter).Trim();
        }

        private string GetSetterAndGetter(PropertyDeclaration property)
        {
            string modifiersLine = "{ ";
            PropertyModifiers modifiers = (PropertyModifiers)property.Modifiers;

            if (property.CanRead)
            {
                modifiersLine += string.Format("{0} get; ", GetModifiers(modifiers.GetterModifiers)).TrimStart();                    
            }

            if (property.CanWrite)
            {
                modifiersLine += string.Format("{0} set; ", GetModifiers(modifiers.SetterModifiers)).TrimStart();
            }

            modifiersLine += "}";
            return modifiersLine;
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
