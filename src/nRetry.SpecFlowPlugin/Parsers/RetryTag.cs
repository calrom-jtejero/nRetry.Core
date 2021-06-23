// -----------------------------------------------------------------------
// <copyright file="RetryTag.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace NRetry.SpecFlowPlugin.Parsers
{
    using System;

    public sealed class RetryTag : IEquatable<RetryTag>
    {
        public RetryTag(int? maxRetries, int? delayBetweenRetriesMs)
        {
            this.MaxRetries = maxRetries;
            this.DelayBetweenRetriesMs = delayBetweenRetriesMs;
        }

        public int? MaxRetries { get; }

        public int? DelayBetweenRetriesMs { get; }

        public bool Equals(RetryTag other)
        {
            if (other is null)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.MaxRetries == other.MaxRetries && this.DelayBetweenRetriesMs == other.DelayBetweenRetriesMs;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((RetryTag)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.MaxRetries.GetHashCode() * 397) ^ this.DelayBetweenRetriesMs.GetHashCode();
            }
        }
    }
}
