using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    class FieldBuilder : IBuilder
    {
        private readonly FieldInfo _fieldInfo;

        public FieldBuilder(MemberInfo memberInfo)
        {
            _fieldInfo = (FieldInfo)memberInfo;
        }

        public object Build()
        {
            string name = _fieldInfo.Name;
            string typeName = _fieldInfo.FieldType.Name;
            bool isGeneric = _fieldInfo.FieldType.IsGenericType;
            Modifiers modifiers = GetModifiers();
            List<string> genericParameters = new List<string>();

            if (isGeneric)
            {
                genericParameters = GetGenericParameters();
            }

            return new FieldDeclaration(name, typeName, isGeneric, modifiers, genericParameters);
        }

        private List<string> GetGenericParameters()
        {
            List<string> genericParameters = new List<string>();

            IEnumerable<Type> genericArguments = _fieldInfo.FieldType.GetGenericTypeDefinition().GetGenericArguments();
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

            FieldAttributes attributes = _fieldInfo.Attributes;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();

            FieldAttributes accessAttributes = attributes & FieldAttributes.FieldAccessMask;
            csharpModifiers = GetAccessModifiers(accessAttributes);

            if ((attributes & FieldAttributes.Literal) != 0)
            {
                csharpModifiers.Add("const");
            }

            if ((attributes & FieldAttributes.Static) != 0)
            {
                csharpModifiers.Add("static");
            }

            if ((attributes & FieldAttributes.InitOnly) != 0)
            {
                csharpModifiers.Add("readonly");
            }

            return new Modifiers(dotnetModifiers, csharpModifiers);
        }

        private List<string> GetAccessModifiers(FieldAttributes fieldAttributes)
        {
            List<string> accessModifiers = new List<string>();

            switch (fieldAttributes)
            {
                case FieldAttributes.Public:
                    accessModifiers.Add("public");
                    break;
                case FieldAttributes.Private:
                    accessModifiers.Add("private");
                    break;
                case FieldAttributes.Assembly:
                    accessModifiers.Add("internal");
                    break;
                case FieldAttributes.Family:
                    accessModifiers.Add("protected");
                    break;
                case FieldAttributes.FamANDAssem:
                    accessModifiers.Add("private protected");
                    break;
                case FieldAttributes.FamORAssem:
                    accessModifiers.Add("protected internal");
                    break;
            }

            return accessModifiers;
        }
    }
}
