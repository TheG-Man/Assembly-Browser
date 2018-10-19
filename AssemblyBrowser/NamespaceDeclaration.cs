using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class NamespaceDeclaration
    {
        private readonly List<TypeDeclaration> _types;

        public string Name { get; }
        public IEnumerable<TypeDeclaration> Types => _types;

        public NamespaceDeclaration(string name)
        {
            Name = name;

            _types = new List<TypeDeclaration>();
        }

        public void AddType(TypeDeclaration typeDeclaration)
        {
            _types.Add(typeDeclaration);
        }
    }
}
