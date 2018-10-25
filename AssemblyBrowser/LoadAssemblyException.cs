using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public class LoadAssemblyException : Exception
    {
        public LoadAssemblyException()
        {
        }

        public LoadAssemblyException(string message) : base(message)
        {
        }
    }
}
