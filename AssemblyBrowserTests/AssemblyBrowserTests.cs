using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _assemblyInfo = _assemblyBrowser.GetAssemblyInfo("C:\\Users\\nikol\\iCloudDrive\\Programming\\C#\\Projects\\MPP\\DTO-Generator\\DtoGenerator\\bin\\Debug\\DtoGenerator.dll");
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
    }
}
