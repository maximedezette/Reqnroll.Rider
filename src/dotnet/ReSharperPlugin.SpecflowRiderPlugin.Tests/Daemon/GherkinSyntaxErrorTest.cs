using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.TestFramework;
using NUnit.Framework;
using ReSharperPlugin.SpecflowRiderPlugin.Formatting;
using ReSharperPlugin.SpecflowRiderPlugin.Psi;

namespace ReSharperPlugin.SpecflowRiderPlugin.Tests.Daemon
{
    [TestFileExtension(GherkinProjectFileType.FEATURE_EXTENSION)]
    [TestSettingsKey(typeof(GherkinFormatSettingsKey))]
    public class GherkinSyntaxErrorTest : HighlightingTestBase
    {
        protected override PsiLanguageType CompilerIdsLanguage => GherkinLanguage.Instance;
        protected override string RelativeTestDataPath => "Daemon/SyntaxError";

        [TestCase("MissingFeatureLine")]
        [TestCase("MissingStepKeyword")]
        [TestCase("MissingScenarioKeyword")]
        public void TestFormat(string name) { DoOneTest(name); }
    }
}