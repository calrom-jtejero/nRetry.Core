// -----------------------------------------------------------------------
// <copyright file="RetrySteps.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace Tests.SpecFlow.Steps
{
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public static class RetrySteps
    {
        private static int retryCount = 0;

        [When(@"I increment the retry count")]
        public static void WhenIIncrementTheRetryCount()
        {
            retryCount++;
        }

        [Then(@"the result should be (.*)")]
        public static void ThenTheResultShouldBe(int expected)
        {
            Assert.AreEqual(expected, retryCount);
        }
    }
}
