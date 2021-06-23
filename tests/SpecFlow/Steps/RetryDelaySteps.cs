// -----------------------------------------------------------------------
// <copyright file="RetryDelaySteps.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace Tests.SpecFlow.Steps
{
    using System.Diagnostics;
    using NUnit.Framework;
    using TechTalk.SpecFlow;

    [Binding]
    public class RetryDelaySteps
    {
        private static Stopwatch stopwatch;

        [When(@"I start the stopwatch if not already started")]
        public static void WhenIStartTheStopwatchIfNotAlreadyStarted()
        {
            if (stopwatch == null)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
        }

        [Then(@"the stopwatch elapsed milliseconds is greater than or equal to '(.*)'")]
        public static void ThenTheStopwatchElapsedMillisecondsIsGreaterThan(int minElapsedMs)
        {
            Assert.True(stopwatch.ElapsedMilliseconds >= minElapsedMs);
        }
    }
}
