using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;
using AssemblyBrowser.Builders;

namespace AssemblyBrowser
{
    public class AssemblyBrowser : IAssemblyBrowser
    {
        private static volatile AssemblyBrowser _instance;
        private static readonly object _syncRoot = new object();

        private readonly List<string> _typesWithExtensionMethods = new List<string>();

        private AssemblyBrowser()
        { 
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
            var assemblyInfo = new AssemblyInfo();
            Assembly assembly = null;

            try
            {
                assembly = Assembly.LoadFrom(path);
            }
            catch (Exception e)
            {
                throw new LoadAssemblyException(e.Message);
            }

            Dictionary<string, MethodDeclaration> extensionMethods = GetExtensionMethods(assembly);

            foreach (TypeInfo definedType in assembly.DefinedTypes)
            {
                if (!definedType.IsNested)
                {
                    if (!_typesWithExtensionMethods.Contains(definedType.Name))
                    {
                        NamespaceDeclaration namespaceDeclaration = new NamespaceDeclaration(definedType.Namespace);

                        var buildDirector = new BuildDirector(new TypeBuilder(definedType));
                        TypeDeclaration typeDeclaration = (TypeDeclaration)buildDirector.Construct(extensionMethods);

                        namespaceDeclaration.AddType(typeDeclaration);
                        assemblyInfo.AddNamespace(namespaceDeclaration);
                    }
                }
            }

            _typesWithExtensionMethods.Clear();

            return assemblyInfo;
        }

        private Dictionary<string, MethodDeclaration> GetExtensionMethods(Assembly assembly)
        {
            Dictionary<string, MethodDeclaration> extensionMethods = new Dictionary<string, MethodDeclaration>();

            foreach (TypeInfo definedType in assembly.DefinedTypes)
            {
                if (!definedType.IsNested)
                {
                    foreach (MethodInfo method in definedType.DeclaredMethods)
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        Type parameterType = parameters.Count() > 0 ? parameters.First().ParameterType : null;

                        if (parameterType != null)
                        {
                            if (method.IsStatic && (parameterType.IsClass || parameterType.IsInterface))
                            {
                                _typesWithExtensionMethods.Add(definedType.Name);

                                var buildDirector = new BuildDirector(new MethodBuilder(method));
                                extensionMethods.Add(parameterType.Name, (MethodDeclaration)buildDirector.Construct());
                            }
                        }
                    }
                }
            }

            return extensionMethods;
        }
    }
}
