using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser;

namespace AssemblyBrowserWPF.ViewModel
{
    class NamespaceViewModel
    {
        private readonly NamespaceDeclaration _namespaceDeclaration;

        public string StringRepresentation => _namespaceDeclaration.Name;
        public IEnumerable<TypeViewModel> Types
        {
            get
            {
                foreach (TypeDeclaration typeDeclaration in _namespaceDeclaration.Types)
                {
                    yield return new TypeViewModel(typeDeclaration);
                }
            }
        }

        public NamespaceViewModel(NamespaceDeclaration namespaceDeclaration)
        {
            _namespaceDeclaration = namespaceDeclaration;
        }
    }
}
