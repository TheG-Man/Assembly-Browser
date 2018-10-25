using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser;

namespace AssemblyBrowserWPF.ViewModel
{
    class AssemblyViewModel
    {
        private readonly AssemblyInfo _assemblyInfo;

        public string StringRepresentation => GetStringRepresentation();
        public IEnumerable<NamespaceViewModel> Namespaces
        {
            get
            {
                foreach (NamespaceDeclaration ns in _assemblyInfo.Namespaces)
                {
                    yield return new NamespaceViewModel(ns);
                }
            }
        }

        public AssemblyViewModel(AssemblyInfo assemblyInfo)
        {
            _assemblyInfo = assemblyInfo;
        }

        private string GetStringRepresentation()
        {
            return "";
        }
    }
}
