﻿using Pyrite.ActionsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyrite.MainDomain;
using Pyrite.ActionsDomain.ValueTypes;
using Pyrite.ActionsDomain.Attributes;

namespace Pyrite.CoreActions.CoreActions
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

        public string Value
        {
            get
            {
                return string.Empty;
            }
            set
            {
                _scenario.LastValue = InputValue.Value;
            }
        }

        public ActionsDomain.ValueTypes.AbstractValueType ValueType
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
        
        public void UserInitializeWith<T>() where T : AbstractValueType
        {
            //do nothing
        }
    }
}
