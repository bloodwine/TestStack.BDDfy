using System;
using System.Collections.Generic;
using System.Linq;

namespace Bddify.Core
{
    public class Scenario
    {
        public Scenario(object testClass, IEnumerable<ExecutionStep> steps, string scenarioText, object[] argsSet = null)
        {
            Object = testClass;
            _steps = steps.ToList();
            Id = Guid.NewGuid();

            ScenarioText = scenarioText;
            _argsSet = argsSet;
        }

        public string ScenarioText { get; private set; }
        public TimeSpan Duration { get; set; }
        public object Object { get; set; }
        public Guid Id { get; private set; }
        private object[] _argsSet;
        public object[] ArgsSet
        {
            get { return _argsSet; }
        }

        private readonly List<ExecutionStep> _steps;
        public IEnumerable<ExecutionStep> Steps
        {
            get { return _steps; }
        }

        public StepExecutionResult Result
        {
            get
            {
                return (StepExecutionResult)Steps.Max(s => (int)s.Result);
            }
        }
    }
}