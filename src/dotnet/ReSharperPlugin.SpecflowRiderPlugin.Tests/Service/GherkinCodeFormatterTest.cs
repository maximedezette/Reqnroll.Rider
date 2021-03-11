using JetBrains.ReSharper.FeaturesTestFramework.Formatter;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using ReSharperPlugin.SpecflowRiderPlugin.Formatting;
using ReSharperPlugin.SpecflowRiderPlugin.Psi;

namespace ReSharperPlugin.SpecflowRiderPlugin.Tests.Service
{
    [TestFileExtension(GherkinProjectFileType.FEATURE_EXTENSION)]
    [TestSettingsKey(typeof(GherkinFormatSettingsKey))]
    public class GherkinCodeFormatterTest : CodeFormatterWithExplicitSettingsTestBase<GherkinLanguage>
    {
        protected override string RelativeTestDataPath => "Formatting";

        [TestCase("Indent - Basics")]
        [TestCase("Indent - MultipleScenario")]
        public void TestFormat(string name) { DoOneTest(name); }
    }
}