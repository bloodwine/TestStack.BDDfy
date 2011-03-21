using System.Linq;
using Bddify.Core;
using NUnit.Framework;

namespace Bddify.Tests.BddifySpecs.Exceptions
{
    public class InconclusiveExceptionContext
    {
        public class InconclusiveTestClass
        {
            public void GivenAClassUnderTest()
            {
            }

            public void WhenInconclusiveExceptionIsThrownInOneOfTheMethods()
            {
            }

            public void ThenTheContextIsFlaggedAsInconclusive()
            {
                Assert.Inconclusive();
            }
        }

        Bddifier _bddifier;

        InconclusiveTestClass TestClass
        {
            get
            {
                return (InconclusiveTestClass)_bddifier.Scenarios.First().Object;
            }
        }

        ExecutionStep GivenStep
        {
            get
            {
                return _bddifier.Scenarios.First().Steps.First(s => s.Method == Helpers.GetMethodInfo(TestClass.GivenAClassUnderTest));
            }
        }

        ExecutionStep WhenStep
        {
            get
            {
                return _bddifier.Scenarios.First().Steps.First(s => s.Method == Helpers.GetMethodInfo(TestClass.WhenInconclusiveExceptionIsThrownInOneOfTheMethods));
            }
        }

        ExecutionStep ThenStep
        {
            get
            {
                return _bddifier.Scenarios.First().Steps.First(s => s.Method == Helpers.GetMethodInfo(TestClass.ThenTheContextIsFlaggedAsInconclusive));
            }
        }


        [SetUp]
        public void InconclusiveExceptionSetup()
        {
            var testClass = new InconclusiveTestClass();
            _bddifier = testClass.LazyBddify(); 
            Assert.Throws<InconclusiveException>(() => _bddifier.Run());
        }

        [Test]
        public void ResultIsInconclusive()
        {
            Assert.That(_bddifier.Scenarios.First().Result, Is.EqualTo(StepExecutionResult.Inconclusive));
        }

        [Test]
        public void ThenIsFlaggedAsInconclusive()
        {
            Assert.That(ThenStep.Result, Is.EqualTo(StepExecutionResult.Inconclusive));
        }

        [Test]
        public void ThenHasAnInconclusiveExceptionOnIt()
        {
            Assert.That(ThenStep.Exception, Is.AssignableFrom(typeof(InconclusiveException)));
        }

        [Test]
        public void GivenIsFlaggedAsSuccessful()
        {
            Assert.That(GivenStep.Result, Is.EqualTo(StepExecutionResult.Passed));
        }

        [Test]
        public void WhenIsFlaggedAsSuccessful()
        {
            Assert.That(WhenStep.Result, Is.EqualTo(StepExecutionResult.Passed));
        }
    }
}