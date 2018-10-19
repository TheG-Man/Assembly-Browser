using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowser.Builders
{
    class EventBuilder : IBuilder
    {
        private readonly EventInfo _eventInfo;

        public EventBuilder(MemberInfo memberInfo)
        {
            _eventInfo = (EventInfo)memberInfo;
        }

        public object Build()
        {
            string name = _eventInfo.Name;
            string typeName = _eventInfo.EventHandlerType.Name;

            Modifiers modifiers = GetModifiers();

            return new EventDeclaration(name, typeName, modifiers);
        }

        private Modifiers GetModifiers()
        {
            List<string> dotnetModifiers = new List<string>();
            List<string> csharpModifiers = new List<string>();

            EventAttributes attributes = _eventInfo.Attributes;
            dotnetModifiers = attributes.ToString().Split(',').ToList();
            dotnetModifiers = dotnetModifiers.Select(s => s.Trim().ToLower()).ToList();

            MethodAttributes visibility = _eventInfo.AddMethod.Attributes & MethodAttributes.MemberAccessMask;
            csharpModifiers = GetAccessModifiers(visibility);

            if ((_eventInfo.AddMethod.Attributes & MethodAttributes.Abstract) != 0)
            {
                csharpModifiers.Add("abstract");
            }

            if ((_eventInfo.AddMethod.Attributes & MethodAttributes.Final) != 0)
            {
                csharpModifiers.Add("sealed");
            }

            if ((_eventInfo.AddMethod.Attributes & MethodAttributes.Virtual) != 0)
            {
                csharpModifiers.Add("virtual");
            }

            if ((_eventInfo.AddMethod.Attributes & MethodAttributes.Static) != 0)
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
