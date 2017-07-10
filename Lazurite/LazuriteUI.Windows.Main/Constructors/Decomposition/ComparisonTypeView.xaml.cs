﻿using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.CoreActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lazurite.MainDomain;
using Lazurite.ActionsDomain;

namespace LazuriteUI.Windows.Main.Constructors.Decomposition
{
    /// <summary>
    /// Логика взаимодействия для ComparisonTypeView.xaml
    /// </summary>
    public partial class ComparisonTypeView : UserControl, IConstructorElement
    {
        private CheckerAction _checkerAction;

        public ComparisonTypeView()
        {
            InitializeComponent();

            button.Click += (o, e) => {
                ComparisonTypeSelectView.Show(
                    (type) => {
                        _checkerAction.ComparisonType = type;
                        Refresh(_checkerAction);
                        Modified?.Invoke(this);
                    },
                    _checkerAction.ComparisonType,
                    _checkerAction.TargetAction1Holder.Action.ValueType.SupportsNumericalComparisons);
            };
        }

        public ActionHolder ActionHolder
        {
            get;set;
        }

        public bool EditMode
        {
            get;
            set;
        }

        public IAlgorithmContext AlgorithmContext
        {
            get;
            set;
        }

        public event Action<IConstructorElement> Modified;
        public event Action<IConstructorElement> NeedAddNext;
        public event Action<IConstructorElement> NeedRemove;

        public void Refresh(CheckerAction checkerAction)
        {
            _checkerAction = checkerAction;
            textBlock.Text = checkerAction.Caption;
        }
    }
}
