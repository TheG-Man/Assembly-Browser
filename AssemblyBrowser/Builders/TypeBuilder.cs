using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    public sealed class TypeBuilder : IBuilder
    {
        private readonly TypeInfo _typeInfo;

        public TypeBuilder(TypeInfo typeInfo)
        {
            _typeInfo = typeInfo;
        }

        public object Build()
        {
            return null;
        }

        public object Build(Dictionary<string, MethodDeclaration> extensionMethods)
        {
            string name = _typeInfo.Name;
            string fullName = _typeInfo.FullName;
            string baseType = _typeInfo.BaseType?.FullName;
            bool isInterface = _typeInfo.IsInterface;
            bool isGeneric = _typeInfo.IsGenericType;

            Modifiers modifiers = GetModifiers();
            List<string> implementedInterfaces = _typeInfo.ImplementedInterfaces.Select(i => i.Name).ToList();

            List<string> genericParameters = new List<string>();

            if (isGeneric)
            {
                genericParameters = GetGenericParameters();
            }

            List<FieldDeclaration> fields = new List<FieldDeclaration>();
            List<PropertyDeclaration> properties = new List<PropertyDeclaration>();
            List<MethodDeclaration> methods = new List<MethodDeclaration>();
            List<EventDeclaration> events = new List<EventDeclaration>();

            List<TypeDeclaration> nestedTypes = new List<TypeDeclaration>();

            foreach (MemberInfo member in _typeInfo.DeclaredMembers)
            {
                if (member is FieldInfo)
                {
                    var buildDirector = new BuildDirector(new FieldBuilder(member));
                    fields.Add((FieldDeclaration)buildDirector.Construct());
                }
                else if (member is PropertyInfo)
                {
                    var buildDirector = new BuildDirector(new PropertyBuilder(member));
                    properties.Add((PropertyDeclaration)buildDirector.Construct());
                }
                else if (member is MethodBase)
                {
                    var buildDirector = new BuildDirector(new MethodBuilder(member));
                    methods.Add((MethodDeclaration)buildDirector.Construct());
                }
                else if (member is EventInfo)
                {
                    var buildDirector = new BuildDirector(new EventBuilder(member));
                    events.Add((EventDeclaration)buildDirector.Construct());
                }
            }

            foreach (KeyValuePair<string, MethodDeclaration> extensionMethod in extensionMethods)
            {
                if (name == extensionMethod.Key)
                {
                    methods.Add(extensionMethod.Value);
                }
            }

            foreach (TypeInfo nestedType in _typeInfo.DeclaredNestedTypes)
            {
                var buildDirector = new BuildDirector(new TypeBuilder(nestedType));
                nestedTypes.Add((TypeDeclaration)buildDirector.Construct(extensionMethods));
            }

            return new TypeDeclaration(name, fullName, baseType, isInterface, isGeneric, modifiers, implementedInterfaces, genericParameters, fields, properties, methods, events, nestedTypes);
        }

        private List<string> GetGenericParameters()
        {
            List<string> genericParameters = new List<string>();

            IEnumerable<Type> genericArguments = _typeInfo.GetGenericTypeDefinition().GetGenericArguments();
            foreach (Type genericArgument in genericArguments)
            {
                genericParameters.Add(genericArgument.Name);
            }

            return genericParameters;
        }

        private Modifiers GetModifiers()
        {
            List<string> dotnetModifiers = new List<string>();
            List<string> csharpModifiers = new List<string>();

            TypeAttributes attributes = _typeInfo.Attributes & ~TypeAttributes.ClassSemanticsMask;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();
            
            TypeAttributes accessAttributes = attributes & TypeAttributes.VisibilityMask;
            csharpModifiers.AddRange(GetAccessModifiers(accessAttributes));

            if ((attributes & TypeAttributes.Abstract) != 0)
            {
                csharpModifiers.Add("abstract");
            }

            if ((attributes & TypeAttributes.Sealed) != 0)
            {
               csharpModifiers.Add("sealed");
            }

            return new Modifiers(dotnetModifiers, csharpModifiers);
        }

        private List<string> GetAccessModifiers(TypeAttributes typeAttributes)
        {
            List<string> accessModifiers = new List<string>();

            switch (typeAttributes)
            {
                case TypeAttributes.NotPublic:
                    accessModifiers.Add("private");
                    break;
                case TypeAttributes.Public:
                    accessModifiers.Add("public");
                    break;
                case TypeAttributes.NestedPublic:
                    accessModifiers.Add("public");
                    break;
                case TypeAttributes.NestedPrivate:
                    accessModifiers.Add("private");
                    break;
                case TypeAttributes.NestedFamANDAssem:
                    accessModifiers.Add("private protected");
                    break;
                case TypeAttributes.NestedAssembly:
                    accessModifiers.Add("internal");
                    break;
                case TypeAttributes.NestedFamily:
                    accessModifiers.Add("protected");
                    break;
                case TypeAttributes.NestedFamORAssem:
                    accessModifiers.Add("protected internal");
                    break;
            }

            return accessModifiers;
        }
    }
}
