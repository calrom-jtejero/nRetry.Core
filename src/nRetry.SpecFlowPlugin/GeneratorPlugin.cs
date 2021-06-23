// -----------------------------------------------------------------------
// <copyright file="GeneratorPlugin.cs" company="Calrom Ltd.">
// Under MIT license
// </copyright>
// -----------------------------------------------------------------------

using NRetry.SpecFlowPlugin;
using NRetry.SpecFlowPlugin.Parsers;
using TechTalk.SpecFlow.Generator.Plugins;
using TechTalk.SpecFlow.Generator.UnitTestProvider;
using TechTalk.SpecFlow.Infrastructure;
using TechTalk.SpecFlow.UnitTestProvider;

[assembly: GeneratorPlugin(typeof(GeneratorPlugin))]

namespace NRetry.SpecFlowPlugin
{
    public class GeneratorPlugin : IGeneratorPlugin
    {
        public void Initialize(
            GeneratorPluginEvents generatorPluginEvents,
            GeneratorPluginParameters generatorPluginParameters,
            UnitTestProviderConfiguration unitTestProviderConfiguration)
        {
            generatorPluginEvents.CustomizeDependencies += this.CustomiseDependencies;
        }

        private void CustomiseDependencies(object sender, CustomizeDependenciesEventArgs eventArgs)
        {
            eventArgs.ObjectContainer.RegisterTypeAs<RetryTagParser, IRetryTagParser>();
            eventArgs.ObjectContainer.RegisterTypeAs<TestGeneratorProviderWithRetry, IUnitTestGeneratorProvider>();
        }
    }
}
