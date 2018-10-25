using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserWPF.ViewModel
{
    class MethodViewModel
    {
        private readonly MethodDeclaration _methodDeclaration;

        public string StringRepresentation => GetStringRepresentation();

        public MethodViewModel(MethodDeclaration methodDeclaration)
        {
            _methodDeclaration = methodDeclaration;
        }

        private string GetStringRepresentation()
        {
            string stringRepresentation;
            stringRepresentation = string.Format("{0} {1} {2}", GetModifiers(_methodDeclaration.Modifiers.CSharpModifiers), _methodDeclaration.ReturnTypeName, _methodDeclaration.Name);

            if (_methodDeclaration.IsGeneric)
            {
                stringRepresentation += string.Format("<{0}>", GetModifiers(_methodDeclaration.GenericParameters));
            }

            stringRepresentation += string.Format("({0}) {1}", GetParameters(_methodDeclaration.Parameters), _methodDeclaration.IsExtention ? "extension" : "");

            return stringRepresentation;
        }

        public string GetModifiers(IEnumerable<string> modifiers)
        {
            string modifiersLine = string.Empty;

            foreach (string modifier in modifiers)
            {
                modifiersLine += string.Format("{0} ", modifier);
            }

            return modifiersLine.TrimEnd();
        }

        private string GetParameters(IEnumerable<ParameterDeclaration> parameters)
        {
            string parametersLine = string.Empty;

            foreach (ParameterDeclaration parameter in parameters)
            {
                string parameterName = string.Empty;

                if (parameter.IsGeneric)
                {
                    parameterName = string.Format("{0}<{1}> {2}", parameter.TypeName, GetModifiers(parameter.GenericParameters), parameter.Name);
                }
                else
                {
                    parameterName = string.Format("{0} {1}", parameter.TypeName, parameter.Name);
                }

                parametersLine += string.Format("{0} {1}, ", GetModifiers(parameter.Modifiers.CSharpModifiers), parameterName).TrimStart();
            }

            return parametersLine.Length > 2 ? parametersLine.Substring(0, parametersLine.Length - 2) : parametersLine;
        }
    }
}
