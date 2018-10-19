using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    class PropertyBuilder : IBuilder
    {
        private readonly PropertyInfo _propertyInfo;

        public PropertyBuilder(MemberInfo memberInfo)
        {
            _propertyInfo = (PropertyInfo)memberInfo;
        }

        public object Build()
        {
            string name = _propertyInfo.Name;
            string typeName = _propertyInfo.PropertyType.Name;
            bool isGeneric = _propertyInfo.PropertyType.IsGenericType;
            bool canRead = _propertyInfo.CanRead;
            bool canWrite = _propertyInfo.CanWrite;
            PropertyModifiers modifiers = GetModifiers();
            List<string> genericParameters = new List<string>();

            if (isGeneric)
            {
                genericParameters = GetGenericParameters();
            }

            return new PropertyDeclaration(name, typeName, isGeneric, canRead, canWrite, modifiers, genericParameters);
        }

        private List<string> GetGenericParameters()
        {
            List<string> genericParameters = new List<string>();

            IEnumerable<Type> genericArguments = _propertyInfo.PropertyType.GetGenericTypeDefinition().GetGenericArguments();
            foreach (Type genericArgument in genericArguments)
            {
                genericParameters.Add(genericArgument.Name);
            }

            return genericParameters;
        }

        private PropertyModifiers GetModifiers()
        {
            List<string> dotnetModifiers = new List<string>();
            List<string> csharpModifiers = new List<string>();
            List<string> getterModifiers = new List<string>();
            List<string> setterModifiers = new List<string>();

            PropertyAttributes attributes = _propertyInfo.Attributes;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();

            MethodInfo getMethod = _propertyInfo.GetGetMethod(nonPublic: true);
            MethodInfo setMethod = _propertyInfo.GetSetMethod(nonPublic: true);

            if (getMethod == null)
            {
                MethodAttributes visibility = setMethod.Attributes & MethodAttributes.MemberAccessMask;

                setterModifiers = GetAccessModifiers(visibility);
            }
            else
            {
                if (setMethod == null)
                {
                    MethodAttributes visibility = getMethod.Attributes & MethodAttributes.MemberAccessMask;

                    getterModifiers = GetAccessModifiers(visibility);
                }
                else
                {
                    MethodAttributes setterVisibility = setMethod.Attributes & MethodAttributes.MemberAccessMask;
                    MethodAttributes getterVisibility = getMethod.Attributes & MethodAttributes.MemberAccessMask;

                    setterModifiers = GetAccessModifiers(setterVisibility);
                    getterModifiers = GetAccessModifiers(getterVisibility);

                    if (getMethod.IsStatic && setMethod.IsStatic)
                    {
                        csharpModifiers.Add("static");
                    }

                    if (getMethod.IsVirtual && setMethod.IsVirtual)
                    {
                        csharpModifiers.Add("virtual");
                    }

                    if (getMethod.IsFinal && setMethod.IsFinal)
                    {
                        csharpModifiers.Add("sealed");
                    }

                    if (getMethod.IsAbstract && setMethod.IsAbstract)
                    {
                        csharpModifiers.Add("abstract");
                    }
                }
            }

            return new PropertyModifiers(dotnetModifiers, csharpModifiers, getterModifiers, setterModifiers);
        }

        private List<string> GetAccessModifiers(MethodAttributes methodAttributes)
        {
            List<string> accessModifiers = new List<string>();

            switch (methodAttributes)
            {
                case MethodAttributes.Public:
                    accessModifiers.Add("public");
                    break;
                case MethodAttributes.Private:
                    accessModifiers.Add("private");
                    break;
                case MethodAttributes.Assembly:
                    accessModifiers.Add("internal");
                    break;
                case MethodAttributes.Family:
                    accessModifiers.Add("protected");
                    break;
                case MethodAttributes.FamANDAssem:
                    accessModifiers.Add("private protected");
                    break;
                case MethodAttributes.FamORAssem:
                    accessModifiers.Add("protected internal");
                    break;
            }

            return accessModifiers;
        }
    }
}
