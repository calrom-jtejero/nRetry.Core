// -----------------------------------------------------------------------
// <copyright file="RetryDefaultSteps.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace Tests.SpecFlow.Steps
{
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class RetryDefaultSteps
    {
        private static int retryCount = 0;

        [When(@"I increment the default retry count")]
        public static void WhenIIncrementTheDefaultRetryCount()
        {
            retryCount++;
        }

        [Then(@"the default result should be (.*)")]
        public static void ThenTheDefaultResultShouldBe(int expected)
        {
            Assert.AreEqual(expected, retryCount);
        }
    }
}
