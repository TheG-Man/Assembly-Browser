using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.ViewModel
{
    class EventViewModel
    {
        private readonly EventDeclaration _eventDeclaration;

        public string StringRepresentation => GetStringRepresentation();

        public EventViewModel(EventDeclaration eventDeclaration)
        {
            _eventDeclaration = eventDeclaration;
        }

        private string GetStringRepresentation()
        {
            string stringRepresentation = string.Empty;

            string name = _eventDeclaration.Name;
            string typeName = _eventDeclaration.TypeName;

            stringRepresentation = string.Format("{0} event {1} {2}", GetModifiers(_eventDeclaration.Modifiers.CSharpModifiers), typeName, name);

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
