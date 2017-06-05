﻿using Lazurite.ActionsDomain;
using Lazurite.MainDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.ActionsDomain.Attributes;

namespace Lazurite.CoreActions
{
    [VisualInitialization]
    [OnlyExecute]
    [SuitableValueTypes(typeof(ButtonValueType))]
    [HumanFriendlyName("Пока")]
    public class WhileAction : IAction, IMultipleAction
    {
        public ComplexAction Action { get; set; }
        public ComplexCheckerAction Checker { get; set; }

        public string Caption
        {
            get
            {
                return string.Empty;
            }
            set
            {
                //
            }
        }
        
        private ButtonValueType _valueType = new ButtonValueType();
        public ActionsDomain.ValueTypes.ValueTypeBase ValueType
        {
            get
            {
                return _valueType;
            }
            set
            {
                //
            }
        }

        public bool IsSupportsEvent
        {
            get
            {
                return ValueChanged != null;
            }
        }

        public IAction[] GetAllActionsFlat()
        {
            return new[] { Action, Action }
            .Union(Action.GetAllActionsFlat())
            .Union(Checker.GetAllActionsFlat())
            .ToArray();
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
            return string.Empty;
        }

        public void SetValue(ExecutionContext context, string value)
        {
            while (Checker.Evaluate(context))
            {
                if (context.CancellationToken.IsCancellationRequested)
                    break;
                Action.SetValue(context, string.Empty);
            }
        }

        public event ValueChangedDelegate ValueChanged;
    }
}