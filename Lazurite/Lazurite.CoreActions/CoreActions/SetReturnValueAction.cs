﻿using Lazurite.ActionsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lazurite.MainDomain;
using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.ActionsDomain.Attributes;

namespace Lazurite.CoreActions.CoreActions
{
    [OnlyExecute]
    [VisualInitialization]
    [HumanFriendlyName("ВернутьЗначение")]
    [SuitableValueTypes(true)]
    public class SetReturnValueAction : ICoreAction, IAction, IMultipleAction
    {
        private ScenarioBase _scenario;
        public void SetTargetScenario(ScenarioBase scenario)
        {
            _scenario = scenario;
        }

        public bool IsSupportsEvent
        {
            get
            {
                return ValueChanged != null;
            }
        }

        public ScenarioBase GetTargetScenario()
        {
            return _scenario;
        }

        public string Caption
        {
            get
            {
                return ActionsDomain.Utils.ExtractHumanFriendlyName(InputValue.GetType()) + " " + InputValue.Caption;
            }
            set
            {
                //
            }
        }

        public IAction InputValue { get; set; }

        public string TargetScenarioId
        {
            get; set;
        }
        
        public ValueTypeBase ValueType
        {
            get
            {
                return _scenario.ValueType;
            }
            set
            {
                //
            }
        }

        public void Initialize()
        {
            //do nothing
        }

        public IAction[] GetAllActionsFlat()
        {
            return new[] { InputValue };
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
            context.OutputChanged.Execute(InputValue.GetValue(context));
        }

        public event ValueChangedDelegate ValueChanged;
    }
}