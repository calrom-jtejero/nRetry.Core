// -----------------------------------------------------------------------
// <copyright file="TestGeneratorProviderWithRetry.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

namespace NRetry.SpecFlowPlugin
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using NRetry.SpecFlowPlugin.Parsers;
    using TechTalk.SpecFlow.Generator;
    using TechTalk.SpecFlow.Generator.CodeDom;
    using TechTalk.SpecFlow.Generator.UnitTestProvider;

    public class TestGeneratorProviderWithRetry : NUnit3TestGeneratorProvider
    {
        private const string IGNORETAG = "ignore";

        private readonly IRetryTagParser retryTagParser;

        public TestGeneratorProviderWithRetry(CodeDomHelper codeDomHelper, IRetryTagParser retryTagParser)
            : base(codeDomHelper)
        {
            this.retryTagParser = retryTagParser;
        }

        public override void SetTestMethodCategories(
            TestClassGenerationContext generationContext,
            CodeMemberMethod testMethod,
            IEnumerable<string> scenarioCategories)
        {
            // Optimisation: Prevent multiple enumerations
            scenarioCategories = scenarioCategories as string[] ?? scenarioCategories.ToArray();

            base.SetTestMethodCategories(generationContext, testMethod, scenarioCategories);

            // Do not add retries to skipped tests (even if they have the retry attribute) as retrying won't affect the outcome.
            //  This allows for the new (for SpecFlow 3.1.x) implementation that relies on Xunit.SkippableFact to still work, as it
            //  too will replace the attribute for running the test with a custom one.
            if (IsIgnored(generationContext, scenarioCategories))
            {
                return;
            }

            string strRetryTag = GetRetryTag(scenarioCategories);
            if (strRetryTag == null)
            {
                return;
            }

            RetryTag retryTag = this.retryTagParser.Parse(strRetryTag);

            // Remove the original fact or theory attribute
            CodeAttributeDeclaration originalAttribute = testMethod.CustomAttributes.OfType<CodeAttributeDeclaration>()
                .FirstOrDefault(a => a.Name == TEST_ATTR || a.Name == ROW_ATTR);
            if (originalAttribute == null)
            {
                return;
            }

            testMethod.CustomAttributes.Remove(originalAttribute);

            // Add the Retry attribute
            CodeAttributeDeclaration retryAttribute = this.CodeDomHelper.AddAttribute(
                testMethod,
                "NUnit.Framework.Retry");

            if (retryTag.MaxRetries != null)
            {
                retryAttribute.Arguments.Add(
                    new CodeAttributeArgument(new CodePrimitiveExpression(retryTag.MaxRetries)));
            }

            // Copy arguments from the original attribute
            for (int i = 0; i < originalAttribute.Arguments.Count; i++)
            {
                retryAttribute.Arguments.Add(originalAttribute.Arguments[i]);
            }
        }

        private static string StripLeadingAtSign(string s) => s.StartsWith("@") ? s.Substring(1) : s;

        private static bool IsIgnoreTag(string tag) => tag.Equals(IGNORETAG, StringComparison.OrdinalIgnoreCase);

        private static bool IsIgnored(TestClassGenerationContext generationContext, IEnumerable<string> tags) =>
            generationContext.Feature.Tags.Select(t => StripLeadingAtSign(t.Name)).Any(IsIgnoreTag) ||
            tags.Any(IsIgnoreTag);

        private static string GetRetryTag(IEnumerable<string> tags) =>
            tags.FirstOrDefault(t =>
                t.StartsWith(Constants.RETRYTAG, StringComparison.OrdinalIgnoreCase) &&
                //// Is just "retry", or is "retry("... for params
                (t.Length == Constants.RETRYTAG.Length || t[Constants.RETRYTAG.Length] == '('));
    }
}
