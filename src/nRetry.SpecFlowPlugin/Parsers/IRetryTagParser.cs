// -----------------------------------------------------------------------
// <copyright file="IRetryTagParser.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace NRetry.SpecFlowPlugin.Parsers
{
    public interface IRetryTagParser
    {
        RetryTag Parse(string tag);
    }
}
