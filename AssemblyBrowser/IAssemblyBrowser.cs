﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser
{
    public interface IAssemblyBrowser
    {
        AssemblyInfo GetAssemblyInfo(string path);
    }
}
