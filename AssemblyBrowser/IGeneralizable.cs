﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    interface IGeneralizable
    {
        bool IsGeneric { get; }
        IEnumerable<string> GenericParameters { get; }
    }
}
