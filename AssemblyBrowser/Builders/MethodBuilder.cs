using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    class MethodBuilder : IBuilder
    {
        private readonly MethodBase _methodBase;

        public MethodBuilder(MemberInfo memberInfo)
        {
            _methodBase = (MethodBase)memberInfo;
        }

        public object Build()
        {
            string name = string.Empty;
            string returnTypeName = string.Empty;
            bool isConstructor = false;
            bool isGeneric = _methodBase.IsGenericMethod;

            List<string> genericMethodParameters = new List<string>();
            if (isGeneric)
            {
                IEnumerable<Type> args = _methodBase.GetGenericArguments();
                foreach (Type arg in args)
                {
                    genericMethodParameters.Add(arg.Name);
                }
            }

            Modifiers modifiers = GetModifiers();
            List<ParameterDeclaration> parameters = new List<ParameterDeclaration>();

            if (_methodBase is MethodInfo)
            {
                MethodInfo methodInfo = (MethodInfo)_methodBase;

                name = methodInfo.Name;
                returnTypeName = methodInfo.ReturnType.Name;

                foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
                {
                    var buildDirector = new BuildDirector(new ParameterBuilder(parameterInfo));
                    parameters.Add((ParameterDeclaration)buildDirector.Construct());
                }
            }
            else if (_methodBase is ConstructorInfo)
            {
                ConstructorInfo constructorInfo = (ConstructorInfo)_methodBase;

                name = constructorInfo.Name;

                foreach (ParameterInfo parameterInfo in constructorInfo.GetParameters())
                {
                    var buildDirector = new BuildDirector(new ParameterBuilder(parameterInfo));
                    parameters.Add((ParameterDeclaration)buildDirector.Construct());
                }

                isConstructor = true;
            }

            return new MethodDeclaration(name, returnTypeName, isConstructor, isGeneric, false, modifiers, parameters, genericMethodParameters);
        }

        private Modifiers GetModifiers()
        {
            List<string> dotnetModifiers = new List<string>();
            List<string> csharpModifiers = new List<string>();

            MethodAttributes attributes = _methodBase.Attributes & ~MethodAttributes.VtableLayoutMask;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();

            MethodAttributes accessAttributes = attributes & MethodAttributes.MemberAccessMask;
            csharpModifiers = GetAccessModifiers(accessAttributes);

            if ((attributes & MethodAttributes.Abstract) != 0)
            {
                csharpModifiers.Add("abstract");
            }

            if ((attributes & MethodAttributes.Final) != 0)
            {
                csharpModifiers.Add("sealed");
            }

            if ((attributes & MethodAttributes.Virtual) != 0)
            {
                csharpModifiers.Add("virtual");
            }

            if ((attributes & MethodAttributes.Static) != 0)
            {
                csharpModifiers.Add("static");
            }

            return new Modifiers(dotnetModifiers, csharpModifiers);
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
