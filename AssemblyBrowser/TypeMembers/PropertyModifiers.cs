using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class PropertyModifiers : Modifiers
    {
        private readonly List<string> _getterModifiers;
        private readonly List<string> _setterModifiers;

        public IEnumerable<string> GetterModifiers => _getterModifiers;
        public IEnumerable<string> SetterModifiers => _setterModifiers;

        public PropertyModifiers(List<string> dotnetModifiers, List<string> csharpModifiers, List<string> getterModifiers, List<string> setterModifiers)
            :base(dotnetModifiers, csharpModifiers)
        {
            _getterModifiers = getterModifiers;
            _setterModifiers = setterModifiers;
        }
    }
}
