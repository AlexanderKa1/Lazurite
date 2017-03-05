﻿using Pyrite.ActionsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.CoreActions.ComparisonTypes
{
    public class EqualityComparisonType : IComparisonType
    {
        public string Caption
        {
            get
            {
                return "=";
            }
            set
            {
                //
            }
        }

        public bool OnlyForNumbers
        {
            get
            {
                return false;
            }
        }

        public bool Calculate(IAction val1, IAction val2, ExecutionContext context)
        {
            return val1.GetValue(context).Equals(val2.GetValue(context));
        }
    }
}
