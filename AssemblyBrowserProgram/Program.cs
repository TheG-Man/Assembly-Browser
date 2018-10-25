using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserProgram
{
    class Program
    {
        private static readonly IAssemblyBrowser _assemblyBrowser = AssemblyBrowser.AssemblyBrowser.GetInstance();
        static void Main(string[] args)
        {
            AssemblyInfo assemblyInfo = _assemblyBrowser.GetAssemblyInfo("C:\\Users\\nikol\\iCloudDrive\\Programming\\C#\\Projects\\MPP\\DTO-Generator\\DtoGenerator\\bin\\Debug\\DtoGenerator.dll");

            PrintAssemblyInfoDotNet(assemblyInfo);

            Console.WriteLine("--------------------------------------------");

            foreach (NamespaceDeclaration namespaceDeclaration in assemblyInfo.Namespaces)
            {
                Console.WriteLine("{0}", namespaceDeclaration.Name);

                PrintTypesInfo(namespaceDeclaration.Types, 1);
            }

            Console.ReadKey();
        }

        private static void PrintTypesInfo(IEnumerable<TypeDeclaration> types, int level)
        {
            foreach (TypeDeclaration typeDeclaration in types)
            {
                PrintAssemblyInfoCSharp(typeDeclaration, level);
            }
        }

        private static void PrintAssemblyInfoDotNet(AssemblyInfo assemblyInfo)
        {
            foreach (NamespaceDeclaration namespaceDeclaration in assemblyInfo.Namespaces)
            {
                Console.WriteLine("[Namespace] {0}", namespaceDeclaration.Name);

                foreach (TypeDeclaration typeDeclaration in namespaceDeclaration.Types)
                {
                    Console.WriteLine("\t[Type] {0}: {1}", typeDeclaration.Name, GetModifiers(typeDeclaration.Modifiers.DotnetModifiers));

                    if (typeDeclaration.ImplementedInterfaces.Count() > 0)
                    {
                        Console.Write("\timplements ");
                        foreach (string interfaceName in typeDeclaration.ImplementedInterfaces)
                        {
                            Console.Write("{0} ", interfaceName);
                        }
                        Console.WriteLine();
                    }

                    // Type Members
                    foreach (FieldDeclaration field in typeDeclaration.Fields)
                    {
                        Console.WriteLine("\t\t[Field] {0}: {1} {2}", field.Name, GetModifiers(field.Modifiers.DotnetModifiers), field.TypeName);
                    }
                    foreach (MethodDeclaration method in typeDeclaration.Methods)
                    {
                        Console.WriteLine("\t\t[Method] {0}: {1} {2}({3})", method.Name, GetModifiers(method.Modifiers.DotnetModifiers), method.ReturnTypeName, GetParameters(method.Parameters));
                    }
                    foreach (PropertyDeclaration property in typeDeclaration.Properties)
                    {
                        Console.WriteLine("\t\t[Property] {0}: {1} {2}", property.Name, GetModifiers(property.Modifiers.DotnetModifiers), property.TypeName);
                    }
                    foreach (EventDeclaration eventDeclaration in typeDeclaration.Events)
                    {
                        Console.WriteLine("\t\t[Event] {0}: {1} {2}", eventDeclaration.Name, GetModifiers(eventDeclaration.Modifiers.DotnetModifiers), eventDeclaration.TypeName);
                    }
                }
            }
        }

        private static void PrintAssemblyInfoCSharp(TypeDeclaration typeDeclaration, int level)
        {
            string classOrInterfaceKeyword = typeDeclaration.IsInterface ? "interface" : "class"; 
            Console.Write(new string(' ', 4 * level) + "{0} {1} {2}", GetModifiers(typeDeclaration.Modifiers.CSharpModifiers), classOrInterfaceKeyword, typeDeclaration.Name);

            if (typeDeclaration.IsGeneric)
            {
                Console.Write("<{0}>", GetModifiers(typeDeclaration.GenericParameters));
            }

            if (typeDeclaration.ImplementedInterfaces.Count() > 0)
            {
                Console.Write(" : ");
                foreach (string interfaceName in typeDeclaration.ImplementedInterfaces)
                {
                    Console.Write("{0} ", interfaceName);
                }
            }

            Console.WriteLine();

            // Type Members
            foreach (FieldDeclaration field in typeDeclaration.Fields)
            {
                Console.WriteLine(new string(' ', 4 + 4 * level) + "{0} {1} {2}", GetModifiers(field.Modifiers.CSharpModifiers), field.TypeName, field.Name);
            }
            foreach (MethodDeclaration method in typeDeclaration.Methods)
            {
                Console.Write(new string(' ', 4 + 4 * level) + "{0} {1} {2}", GetModifiers(method.Modifiers.CSharpModifiers), method.ReturnTypeName, method.Name);

                if (method.IsGeneric)
                {
                    Console.Write("<{0}>", GetModifiers(method.GenericParameters));
                }

                Console.Write("({0})", GetParameters(method.Parameters));

                if (method.IsExtention)
                {
                    Console.WriteLine(" ----EXTENSION----");
                }
                else
                {
                    Console.WriteLine();
                }
            }
            foreach (PropertyDeclaration property in typeDeclaration.Properties)
            {
                Console.WriteLine(new string(' ', 4 + 4 * level) + "{0} {1} {2} {3}", GetModifiers(property.Modifiers.CSharpModifiers), property.TypeName, property.Name, GetSetterAndGetter(property));
            }
            foreach (EventDeclaration eventDeclaration in typeDeclaration.Events)
            {
                Console.WriteLine(new string(' ', 4 + 4 * level) + "{0} event {1} {2}", GetModifiers(eventDeclaration.Modifiers.CSharpModifiers), eventDeclaration.TypeName, eventDeclaration.Name);
            }
            
            if (typeDeclaration.NestedTypes.Count() > 0)
                PrintTypesInfo(typeDeclaration.NestedTypes, ++level);
        }

        private static string GetModifiers(IEnumerable<string> modifiers)
        {
            string modifiersLine = string.Empty;

            foreach (string modifier in modifiers)
            {
                modifiersLine += string.Format("{0} ", modifier);
            }

            return modifiersLine.TrimEnd();
        }

        private static string GetParameters(IEnumerable<ParameterDeclaration> parameters)
        {
            string parametersLine = string.Empty;

            for (int i = 0; i < parameters.Count() - 1; ++i)
            {
                if (parameters.ElementAt(i).Modifiers.CSharpModifiers.Count() > 0)
                {
                    parametersLine += string.Format("{0} {1} {2}, ", GetModifiers(parameters.ElementAt(i).Modifiers.CSharpModifiers), parameters.ElementAt(i).TypeName, parameters.ElementAt(i).Name);
                }
                else
                {
                    parametersLine += string.Format("{0} {1}, ", parameters.ElementAt(i).TypeName, parameters.ElementAt(i).Name);
                }
            }

            if (parameters.Count() > 0)
            {
                if (parameters.Last().Modifiers.CSharpModifiers.Count() > 0)
                {
                    parametersLine += string.Format("{0} {1} {2}", GetModifiers(parameters.Last().Modifiers.CSharpModifiers), parameters.Last().TypeName, parameters.Last().Name);
                }
                else
                {
                    parametersLine += string.Format("{0} {1}", parameters.Last().TypeName, parameters.Last().Name);
                }
            }

            return parametersLine;
        }

        private static string GetSetterAndGetter(PropertyDeclaration property)
        {
            string modifiersLine = "{";

            /*if (property.GetterModifiers.ElementAt(0) == "readable")
            {
                if (property.GetterModifiers.Count() > 1)
                {
                    for (int i = 1; i < property.GetterModifiers.Count(); ++i)
                    {
                        modifiersLine += string.Format("{0} ", property.GetterModifiers.ElementAt(i));
                    }

                    modifiersLine = modifiersLine.TrimEnd();
                }

                modifiersLine += " get; ";
            }

            if (property.SetterModifiers.ElementAt(0) == "writable")
            {
                if (property.SetterModifiers.Count() > 1)
                {
                    for (int i = 1; i < property.SetterModifiers.Count(); ++i)
                    {
                        modifiersLine += string.Format("{0} ", property.SetterModifiers.ElementAt(i));
                    }
                }

                modifiersLine += "set; ";
            }
            */
            modifiersLine += "}";
            return modifiersLine;
        }
    }
}
