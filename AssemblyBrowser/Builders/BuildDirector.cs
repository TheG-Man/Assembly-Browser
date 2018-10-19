using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.Builders
{
    class BuildDirector
    {
        private readonly IBuilder _builder;

        public BuildDirector(IBuilder builder)
        {
            _builder = builder;
        }

        public object Construct()
        {
            return _builder.Build();
        }
    }
}
