using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Tests.SpecFlow.Steps
{
    [Binding]
    public class RetrySteps
    {
        private static int _retryCount = 0;

        [When(@"I increment the retry count")]
        public static void WhenIIncrementTheRetryCount()
        {
            _retryCount++;
        }

        [Then(@"the result should be (.*)")]
        public static void ThenTheResultShouldBe(int expected)
        {
            Assert.AreEqual(expected, _retryCount);
        }
    }
}
