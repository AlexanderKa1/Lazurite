﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Pyrite.Utils
{
    public static class ReflectionUtils
    {
        public static TypeInfo[] GetAllOfType(Type type, Assembly assembly=null)
        {
            var typeInfo = type.GetTypeInfo();
            if (assembly == null)
                assembly = typeInfo.Assembly;
            return assembly.DefinedTypes
                    .Where(x => typeInfo.IsAssignableFrom(x) && !x.IsInterface).ToArray();
        }
    }
}
