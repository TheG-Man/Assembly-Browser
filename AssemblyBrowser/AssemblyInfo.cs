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

        internal void AddOrCreateNamespace(NamespaceDeclaration namespaceDeclaration)
        {
            NamespaceDeclaration existingNamespace = _namespaces.Find(n => n.Name == namespaceDeclaration.Name);

            if (existingNamespace != null)
            {
                foreach (TypeDeclaration typeDeclaration in namespaceDeclaration.Types)
                {
                    existingNamespace.AddType(typeDeclaration);
                }
            }
            else
            {
                _namespaces.Add(namespaceDeclaration);
            }
        }
    }
}
