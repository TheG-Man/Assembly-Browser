using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.Builders;

namespace AssemblyBrowser
{
    public class AssemblyBrowser : IAssemblyBrowser
    {
        private static volatile AssemblyBrowser _instance;
        private static readonly object _syncRoot = new object();

        private readonly AssemblyInfo _assemblyInfo;

        private AssemblyBrowser()
        {
            _assemblyInfo = new AssemblyInfo();
        }

        public static AssemblyBrowser GetInstance()
        {
            if (_instance == null)
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new AssemblyBrowser();
                    }
                }
            }

            return _instance;
        }

        public AssemblyInfo GetAssemblyInfo(string path)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(path);

                foreach (TypeInfo definedType in assembly.DefinedTypes)
                {
                    if (!definedType.IsNested)
                    {
                        NamespaceDeclaration namespaceDeclaration = new NamespaceDeclaration(definedType.Namespace);

                        var buildDirector = new BuildDirector(new TypeBuilder(definedType));
                        TypeDeclaration typeDeclaration = (TypeDeclaration)buildDirector.Construct();

                        namespaceDeclaration.AddType(typeDeclaration);
                        _assemblyInfo.AddNamespace(namespaceDeclaration);
                    }
                }
            }
            catch (Exception e)
            {
                //Assembly load exception
            }

            return _assemblyInfo;
            }
    }
}
