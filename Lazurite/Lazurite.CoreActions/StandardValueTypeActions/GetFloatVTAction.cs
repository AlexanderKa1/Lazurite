﻿using Lazurite.ActionsDomain;
using Lazurite.ActionsDomain.Attributes;
using Lazurite.ActionsDomain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazurite.CoreActions.StandardValueTypeActions
{
    [VisualInitialization]
    [HumanFriendlyName("# Число")]
    [SuitableValueTypes(typeof(FloatValueType))]
    public class GetFloatVTAction : IAction, IStandardValueAction
    {
        public string Caption
        {
            get
            {
                return Value;
            }
            set
            {
                //
            }
        }

        public string Value
        {
            get;
            set;
        } = "0";
        
        public ActionsDomain.ValueTypes.ValueTypeBase ValueType
        {
            get;
            set;
        } = new FloatValueType();

        public bool IsSupportsEvent
        {
            get
            {
                return ValueChanged != null;
            }
        }

        public bool IsSupportsModification
        {
            get
            {
                return true;
            }
        }

        public void Initialize()
        {
            //
        }

        public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
        {
            return true;
        }

        public string GetValue(ExecutionContext context)
        {
            return Value;
        }

        public void SetValue(ExecutionContext context, string value)
        {
            Value = value;
        }

        public event ValueChangedDelegate ValueChanged;
    }
}