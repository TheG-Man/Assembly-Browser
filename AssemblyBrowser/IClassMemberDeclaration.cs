using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    interface IClassMemberDeclaration
    {
        List<string> Attributes { get; }
        List<string> Modifiers { get; }
        string Type { get; }
        string Name { get; }
    }
}
