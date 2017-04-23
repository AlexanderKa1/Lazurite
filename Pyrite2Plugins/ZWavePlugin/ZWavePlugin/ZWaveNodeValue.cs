﻿using Pyrite.ActionsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pyrite.ActionsDomain.ValueTypes;
using OpenZWrapper;
using Pyrite.ActionsDomain.Attributes;
using ZWavePluginUI;

namespace ZWavePlugin
{
    [HumanFriendlyName("Параметр ZWave устройства")]
    [SuitableValueTypes(
        typeof(StateValueType), typeof(InfoValueType), typeof(FloatValueType), 
        typeof(ButtonValueType), typeof(ToggleValueType))]
    public class ZWaveNodeValue : IAction
    {
        public byte NodeId { get; set; }
        public uint HomeId { get; set; }
        public ulong ValueId { get; set; }

        private NodeValue _nodeValue;

        public string Caption
        {
            get
            {
                return _nodeValue?.Node.Name + " -> " + _nodeValue?.Name;
            }
            set
            {
                //do nothing
            }
        }
        
        public ValueTypeBase ValueType
        {
            get;
            set;
        }

        public bool IsSupportsEvent
        {
            get
            {
                return ValueChanged != null;
            }
        }

        public event ValueChangedDelegate ValueChanged;

        public string GetValue(ExecutionContext context)
        {
            return _nodeValue?.Current.ToString();
        }

        public void Initialize()
        {
            ZWaveManager.Current.WaitForInitialized();
            var nodes = ZWaveManager.Current.GetNodes();
            var node = nodes.FirstOrDefault(x => x.Id.Equals(this.NodeId));
            _nodeValue = node.Values.FirstOrDefault(x => x.Id.Equals(this.ValueId));
            if (_nodeValue != null)
                _nodeValue.Changed += NodeValue_Changed;
        }

        private void NodeValue_Changed(object arg1, NodeValueChangedEventArgs arg2)
        {
            this.ValueChanged?.Invoke(this, _nodeValue.Current.ToString());
        }

        public void SetValue(ExecutionContext context, string value)
        {
            _nodeValue.Current = value;
        }

        public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValueTypes)
        {
            var manager = ZWaveManager.Current;
            var parameterSelectView = new NodesValuesComplexView();
            parameterSelectView.InitializeWith(
                manager, 
                _nodeValue?.Node, 
                _nodeValue, 
                (nodeValue) => ZWaveTypeComparability.IsTypesComparable(nodeValue, valueType, inheritsSupportedValueTypes));
            var window = new ZWaveSelectionWindow(manager);
            window.SetPrimaryControl(parameterSelectView);
            if (window.ShowDialog() ?? false)
            {
                if (_nodeValue != null)
                    _nodeValue.Changed -= NodeValue_Changed;
                _nodeValue = parameterSelectView.SelectedNodeValue;
                NodeId = _nodeValue.Node.Id;
                HomeId = _nodeValue.Node.HomeId;
                ValueId = _nodeValue.Id;
                if (_nodeValue != null)
                    _nodeValue.Changed += NodeValue_Changed;
                return true;
            }
            else return false;
        }
    }
}
