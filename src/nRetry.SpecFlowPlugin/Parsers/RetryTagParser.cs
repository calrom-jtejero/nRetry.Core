// -----------------------------------------------------------------------
// <copyright file="RetryTagParser.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace NRetry.SpecFlowPlugin.Parsers
{
    using System;
    using System.Text.RegularExpressions;

    public class RetryTagParser : IRetryTagParser
    {
        // unescaped: ^retry(\(([0-9]+)(,([0-9]+))?\))?$
        private readonly Regex regex = new Regex(
            $"^{Constants.RETRYTAG}(\\(([0-9]+)(,([0-9]+))?\\))?$",
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public RetryTag Parse(string tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            int? maxRetries;
            int? delayBetweenRetriesMs = null;

            Match match = this.regex.Match(tag);
            if (match.Success && match.Groups[2].Success)
            {
                maxRetries = int.Parse(match.Groups[2].Value);
            }
            else
            {
                maxRetries = 3;
            }

            return new RetryTag(maxRetries, delayBetweenRetriesMs);
        }
    }
}
