using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    class ParameterBuilder : IBuilder
    {
        private readonly ParameterInfo _parameterInfo;

        public ParameterBuilder(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo;
        }

        public object Build()
        {
            string name = _parameterInfo.Name;
            string typeName = _parameterInfo.ParameterType.Name;
            bool isGeneric = _parameterInfo.ParameterType.IsGenericType;
            bool isClass = _parameterInfo.ParameterType.IsClass | _parameterInfo.ParameterType.IsInterface;

            Modifiers modifiers = GetModifiers();
            List<string> genericParameters = new List<string>();

            if (isGeneric)
            {
                genericParameters = GetGenericParameters();
            }

            return new ParameterDeclaration(name, typeName, isGeneric, isClass, modifiers, genericParameters);
        }

        private List<string> GetGenericParameters()
        {
            List<string> genericParameters = new List<string>();

            IEnumerable<Type> genericArguments = _parameterInfo.ParameterType.GetGenericTypeDefinition().GetGenericArguments();
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

            ParameterAttributes attributes = _parameterInfo.Attributes;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();

            if ((attributes & ParameterAttributes.In) != 0)
            {
                csharpModifiers.Add("in");
            }

            if ((attributes & ParameterAttributes.Out) != 0)
            {
                csharpModifiers.Add("out");
            }

            return new Modifiers(dotnetModifiers, csharpModifiers);
        }
    }
}
