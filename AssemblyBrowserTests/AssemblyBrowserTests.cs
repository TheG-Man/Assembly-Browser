using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using AssemblyBrowser;
using AssemblyBrowser.TypeMembers;

namespace AssemblyBrowserTests
{
    [TestFixture]
    public class AssemblyBrowserTests
    {
        private readonly IAssemblyBrowser _assemblyBrowser = AssemblyBrowser.AssemblyBrowser.GetInstance();
        private AssemblyInfo _assemblyInfo;

        [SetUp]
        public void Setup()
        {

            _assemblyInfo = _assemblyBrowser.GetAssemblyInfo(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/../../TestDll/DtoGenerator.dll");
        }

        [Test]
        public void NamespaceCountTest()
        {
            Assert.AreEqual(1, _assemblyInfo.Namespaces.Count());
        }

        [Test]
        public void NamespaceNameTest()
        {
            Assert.AreEqual("DtoGenerator", _assemblyInfo.Namespaces.First().Name);
        }

        [Test]
        public void TypesCountTest()
        {
            Assert.AreEqual(15, _assemblyInfo.Namespaces.First().Types.Count());
        }

        [Test]
        public void HasIDtoGeneratorTypeTest()
        {
            TypeDeclaration iDtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "IDtoGenerator");

            Assert.AreEqual("IDtoGenerator", iDtoGeneratorType?.Name);
        }

        [Test]
        public void ExtensionMethodsTest()
        {
            TypeDeclaration iDtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "IDtoGenerator");

            Assert.AreEqual(true, iDtoGeneratorType.Methods.Last().IsExtention);
        }

        [Test]
        public void DtoGeneratorFieldsCountTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual(4, dtoGeneratorType.Fields.Count());
        }

        [Test]
        public void DtoGeneratorMethodsCountTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual(10, dtoGeneratorType.Methods.Count());
        }

        [Test]
        public void DtoGeneratorPropertiesCountTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual(0, dtoGeneratorType.Properties.Count());
        }

        [Test]
        public void DtoGeneratorEventsCountTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual(0, dtoGeneratorType.Events.Count());
        }

        [Test]
        public void DtoGeneratorCreateObjectViaConstructorMethodParametersCountTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual(2, dtoGeneratorType.Methods.First(x => x.Name == "CreateObjectViaConstructor").Parameters.Count());
        }

        [Test]
        public void DtoGeneratorCreateObjectViaConstructorMethodFirstParameterNameTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual("dtoType", dtoGeneratorType.Methods.First(x => x.Name == "CreateObjectViaConstructor").Parameters.First().Name);
        }

        [Test]
        public void DtoGeneratorImplementedInterfacesTest()
        {
            TypeDeclaration dtoGeneratorType = _assemblyInfo.Namespaces.First().Types.First(x => x.Name == "DtoGenerator");

            Assert.AreEqual("IDtoGenerator", dtoGeneratorType.ImplementedInterfaces.First());
        }
    }
}
