﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lazurite.ActionsDomain;
using Lazurite.Data;
using Lazurite.IOC;
using Lazurite.Scenarios;
using Lazurite.Scenarios.ScenarioTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lazurite.ActionsDomain.ValueTypes;
using Lazurite.CoreActions;
using System.Threading;
using Lazurite.CoreActions.CheckerLogicalOperators;
using Lazurite.CoreActions.ComparisonTypes;
using System.Diagnostics;
using Lazurite.Windows.Logging;

namespace Lazurite.Tests
{
    [TestClass]
    public class ScenariosRepositoryTest
    {
        [TestMethod]
        public void CreateAndSaveScenario()
        {
            Singleton.Add(new WarningHandler());
            Singleton.Add(new FileSavior());
            var repository = new ScenariosRepository();
            var testScen = new SingleActionScenario();
            testScen.Name = "testScen";
            testScen.Category = "category1";
            testScen.TargetAction = new ComplexCheckerActionTest.TestAction() {
                Value = "20"
            };
            repository.AddScenario(testScen);
        }

        [TestMethod]
        public void LoadScenario()
        {
            Singleton.Add(new WarningHandler());
            Singleton.Add(new FileSavior());
            var repository = new ScenariosRepository();
            var testScen = repository.Scenarios.Single(x => x.Name.Equals("testScen"));
            testScen.Execute("30", new System.Threading.CancellationToken());
        }

        [TestMethod]
        public void CreateCompositeScenario()
        {
            Singleton.Add(new WarningHandler());
            Singleton.Add(new FileSavior());
            var repository = new ScenariosRepository();
            Singleton.Add(repository);
            
            var scen = new CompositeScenario();
            scen.TargetAction = new ComplexAction()
            {
                Actions = new List<IAction>()
                {
                    new WhileAction()
                    {
                        Checker = new ComplexCheckerAction()
                        {
                            CheckerOperations = new List<CheckerOperatorPair>() {
                                new CheckerOperatorPair() {
                                    Operator = LogicalOperator.Or,
                                    Checker = new CheckerAction()
                                    {
                                        ComparisonType = new EqualityComparisonType(),
                                        TargetAction1 = new ToggleConstAction(),
                                        TargetAction2 = new ToggleConstAction()
                                    }
                                }
                            }
                        },
                        Action = new ComplexAction()
                        {
                            Actions = new List<IAction>
                            {
                                new ExecuteAction()
                                {
                                    Action = new WriteDataAction(),
                                    InputValue = new GetCurrentDateTimeAction()
                                },
                                new ExecuteAction()
                                {
                                    Action = new WaitAction()
                                }
                            }
                        }
                    }
                }
            };
            repository.AddScenario(scen);
            scen.Execute(string.Empty, new CancellationToken());
        }

        public class WaitAction : IAction
        {
            public bool IsSupportsEvent
            {
                get
                {
                    return ValueChanged != null;
                }
            }

            public string Caption
            {
                get
                {
                    return "wa";
                }

                set
                {
                    //
                }
            }

            public ValueTypeBase ValueType
            {
                get
                {
                    return new ButtonValueType();
                }
                set
                {
                    //
                }
            }

            public string GetValue(ActionsDomain.ExecutionContext context)
            {
                return string.Empty;
            }

            public void Initialize()
            {
                //
            }

            public void SetValue(ActionsDomain.ExecutionContext context, string value)
            {
                Thread.Sleep(8000);
            }

            public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
            {
                return true;
            }

            public event ValueChangedDelegate ValueChanged;
        }
        public class GetCurrentDateTimeAction: IAction
        {
            public bool IsSupportsEvent
            {
                get
                {
                    return ValueChanged != null;
                }
            }

            public string Caption
            {
                get
                {
                    return "curdt";
                }
                set
                {
                    //
                }
            }

            public ValueTypeBase ValueType
            {
                get
                {
                    return new InfoValueType();
                }

                set
                {
                    //
                }
            }

            public string GetValue(ActionsDomain.ExecutionContext context)
            {
                return DateTime.Now.ToString();
            }

            public void Initialize()
            {
                //
            }

            public void SetValue(ActionsDomain.ExecutionContext context, string value)
            {
                //
            }

            public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
            {
                return true;
            }

            public event ValueChangedDelegate ValueChanged;
        }
        public class WriteDataAction : IAction
        {
            public bool IsSupportsEvent
            {
                get
                {
                    return ValueChanged != null;
                }
            }

            public string Caption
            {
                get
                {
                    return "wda";
                }

                set
                {
                    //
                }
            }

            public ValueTypeBase ValueType
            {
                get
                {
                    return new InfoValueType();
                }

                set
                {
                    //
                }
            }

            public string GetValue(ActionsDomain.ExecutionContext context)
            {
                return "";
            }

            public void Initialize()
            {
                //
            }

            public void SetValue(ActionsDomain.ExecutionContext context, string value)
            {
                Debug.WriteLine(value);
            }

            public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
            {
                return false;
            }

            public event ValueChangedDelegate ValueChanged;
        }
        public class ToggleConstAction : IAction
        {
            public bool On { get; set; }
            public ToggleConstAction(bool on)
            {
                On = on;
            }

            public ToggleConstAction()
            {
                On = true;
            }

            public bool IsSupportsEvent
            {
                get
                {
                    return ValueChanged != null;
                }
            }

            public string Caption
            {
                get
                {
                    return "aaa";
                }

                set
                {
                    //
                }
            }

            public ValueTypeBase ValueType
            {
                get
                {
                    return new ToggleValueType();
                }

                set
                {
                    //
                }
            }

            public string GetValue(ActionsDomain.ExecutionContext context)
            {
                return On ? ToggleValueType.ValueON : ToggleValueType.ValueOFF;
            }

            public void Initialize()
            {
                //
            }

            public void SetValue(ActionsDomain.ExecutionContext context, string value)
            {
                //
            }

            public bool UserInitializeWith(ValueTypeBase valueType, bool inheritsSupportedValues)
            {
                return true;
            }

            public event ValueChangedDelegate ValueChanged;
        }
    }
}