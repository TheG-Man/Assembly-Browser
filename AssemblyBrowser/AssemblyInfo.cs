using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public sealed class AssemblyInfo
    {
        private readonly List<NamespaceDeclaration> _namespaces;

        public IEnumerable<NamespaceDeclaration> Namespaces => _namespaces;

        internal AssemblyInfo()
        {
            _namespaces = new List<NamespaceDeclaration>();
        }

        internal void AddNamespace(NamespaceDeclaration namespaceDeclaration)
        {
            NamespaceDeclaration ns = _namespaces.Find(n => n.Name == namespaceDeclaration.Name);

            if (ns != null)
            {
                foreach (TypeDeclaration typeDeclaration in namespaceDeclaration.Types)
                {
                    ns.AddType(typeDeclaration);
                }
            }
            else
            {
                _namespaces.Add(namespaceDeclaration);
            }
        }
    }
}
