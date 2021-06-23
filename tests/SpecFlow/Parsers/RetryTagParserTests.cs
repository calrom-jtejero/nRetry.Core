// -----------------------------------------------------------------------
// <copyright file="RetryTagParserTests.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace Tests.SpecFlow.Parsers
{
    using System;
    using NRetry.SpecFlowPlugin.Parsers;
    using NUnit.Framework;

    public class RetryTagParserTests
    {
        [Test]
        public void Parse_Null_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => GetParser().Parse(null));

        [Test]
        public void Parse_NoParams_CorrectResult()
        {
            // Arrange
            RetryTagParser parser = GetParser();
            RetryTag expected = new RetryTag(3, null);

            // Act
            RetryTag actual = parser.Parse("retry");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("retry(5)", 5)]
        [TestCase("Retry(5)", 5)]
        [TestCase("RETRY(5)", 5)]
        [TestCase("ReTrY(5)", 5)]
        public void Parse_MaxRetries_ReturnsCorrectResult(string tag, int maxRetries)
        {
            // Arrange
            RetryTagParser parser = GetParser();
            RetryTag expected = new RetryTag(maxRetries, null);

            // Act
            RetryTag actual = parser.Parse(tag);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("retry(5,100)", 5, null)]
        [TestCase("Retry(5,100)", 5, null)]
        [TestCase("RETRY(5,100)", 5, null)]
        [TestCase("rEtRy(5,100)", 5, null)]
        [TestCase("retry(765,87)", 765, null)]
        public void Parse_MaxRetriesAndDelayBetweenRetriesMs_ReturnsCorrectResult(
            string tag,
            int maxRetries,
            int? delayBetweenRetriesMs)
        {
            // Arrange
            RetryTagParser parser = GetParser();
            RetryTag expected = new RetryTag(maxRetries, delayBetweenRetriesMs);

            // Act
            RetryTag actual = parser.Parse(tag);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private static RetryTagParser GetParser() => new RetryTagParser();
    }
}
