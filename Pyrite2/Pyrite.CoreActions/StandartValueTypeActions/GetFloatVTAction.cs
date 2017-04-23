﻿using Pyrite.ActionsDomain;
using Pyrite.ActionsDomain.Attributes;
using Pyrite.ActionsDomain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pyrite.CoreActions.StandartValueTypeActions
{
    [OnlyGetValue]
    [VisualInitialization]
    [SuitableValueTypes(typeof(FloatValueType))]
    public class GetFloatVTAction : IAction
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
        }

        private FloatValueType _valueType;
        public ActionsDomain.ValueTypes.ValueTypeBase ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                _valueType = (FloatValueType)value;
            }
        }

        public bool IsSupportsEvent
        {
            get
            {
                return ValueChanged != null;
            }
        }

        public void Initialize()
        {
            //
        }

        public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
        {
            return false;
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
