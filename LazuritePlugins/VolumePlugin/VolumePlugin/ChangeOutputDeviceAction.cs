﻿using Lazurite.ActionsDomain;
using Lazurite.ActionsDomain.Attributes;
using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.Shared.ActionCategory;
using LazuriteUI.Icons;

namespace VolumePlugin
{
    [LazuriteIcon(Icon.Sound3)]
    [HumanFriendlyName("Устройство воспроизведения")]
    [SuitableValueTypes(typeof(FloatValueType))]
    [Category(Category.Multimedia)]
    public class ChangeOutputDeviceAction : IAction
    {
        public string Caption
        {
            get => string.Empty;

            set
            {
                //do nothing
            }
        }

        public bool IsSupportsEvent => false;

        public bool IsSupportsModification => false;

        public ValueTypeBase ValueType
        {
            get;
            set;
        } = new FloatValueType()
        {
            AcceptedValues = new[] { "0", "100" }
        };

        public event ValueChangedEventHandler ValueChanged;

        public string GetValue(ExecutionContext context) => 
            Utils.GetDefaultOutputDeviceIndex().ToString();

        public void Initialize()
        {
            //do nothing
        }

        public void SetValue(ExecutionContext context, string value) =>
            Utils.SetOutputAudioDevice((int)double.Parse(value));

        public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues) => true;
    }
}
