using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.TypeMembers
{
    public class Modifiers
    {
        private readonly List<string> _dotnetModifiers;
        private readonly List<string> _csharpModifiers;

        public IEnumerable<string> DotnetModifiers => _dotnetModifiers;
        public IEnumerable<string> CSharpModifiers => _csharpModifiers;

        public Modifiers(List<string> dotnetModifiers, List<string> csharpModifiers)
        {
            _dotnetModifiers = dotnetModifiers;
            _csharpModifiers = csharpModifiers;
        }
    }
}
