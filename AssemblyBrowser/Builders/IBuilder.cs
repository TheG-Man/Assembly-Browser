using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    interface IBuilder
    {
        object Build();
    }

    public static class BuilderExtensions
    {
        public static object Build(this TypeBuilder typeBuilder, Dictionary<string, MethodDeclaration> extensionMethods)
        {
            return typeBuilder.Build(extensionMethods);
        }
    }
}
