﻿using System.Linq;
using Lazurite.MainDomain;
using Lazurite.IOC;
using Lazurite.ActionsDomain;
using System;
using System.Threading;
using Lazurite.ActionsDomain.Attributes;
using Lazurite.ActionsDomain.ValueTypes;

namespace Lazurite.CoreActions.CoreActions
{
    [HumanFriendlyName("Выполнить существующий сценарий")]
    [VisualInitialization]
    [OnlyExecute]
    [SuitableValueTypes(true)]
    public class RunExistingScenarioAction : ICoreAction, IAction
    {
        public string TargetScenarioId
        {
            get; set;
        }

        private ScenarioBase _scenario;
        public void SetTargetScenario(ScenarioBase scenario)
        {
            _scenario = scenario;
        }

        public bool IsSupportsEvent
        {
            get
            {
                return false;
            }
        }

        public ScenarioBase GetTargetScenario()
        {
            return _scenario;
        }

        public ActionHolder InputValue { get; set; } = new ActionHolder();
        
        public string Caption
        {
            get
            {
                return _scenario?.Name ?? "[сценарий не выбран]";
            }
            set
            {
                //
            }
        }

        public bool IsSupportsModification
        {
            get
            {
                return true;
            }
        }

        public ValueTypeBase ValueType
        {
            get
            {
                return _scenario?.ValueType ?? new ButtonValueType();
            }
            set
            {
                //do nothing
            }
        }

        public RunExistingScenarioMode Mode
        {
            get;
            set;
        } = RunExistingScenarioMode.Synchronously;

        public void Initialize()
        {
            //do nothing
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
            if (Mode == RunExistingScenarioMode.Asynchronously)
                _scenario?.ExecuteAsyncParallel(InputValue.Action.GetValue(context), context.CancellationToken);
            else if (Mode == RunExistingScenarioMode.Synchronously)
                _scenario?.Execute(InputValue.Action.GetValue(context), context.CancellationToken);
            else if (Mode == RunExistingScenarioMode.MainExecutionContext)
                _scenario?.ExecuteAsync(InputValue.Action.GetValue(context));
        }

        public event ValueChangedDelegate ValueChanged;
    }
}