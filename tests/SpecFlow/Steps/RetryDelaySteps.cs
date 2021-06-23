using System.Diagnostics;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Tests.SpecFlow.Steps
{
    [Binding]
    public class RetryDelaySteps
    {
        private static Stopwatch _stopwatch;

        [When(@"I start the stopwatch if not already started")]
        public static void WhenIStartTheStopwatchIfNotAlreadyStarted()
        {
            if (_stopwatch == null)
            {
                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }
        }

        [Then(@"the stopwatch elapsed milliseconds is greater than or equal to '(.*)'")]
        public static void ThenTheStopwatchElapsedMillisecondsIsGreaterThan(int minElapsedMs)
        {
            Assert.True(_stopwatch.ElapsedMilliseconds >= minElapsedMs);
        }

    }
}
