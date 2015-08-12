using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;

namespace PopupTranslator.Tests
{
    [TestFixture]
    public class TranslatorTests
    {
        [SetUp]
        public void BeforeEachTest()
        {
            supportedLanguage = new Language("SupportedLanguage", "SL");
            unsupportedLanguage = new Language("UnsupportedLanguage", "UL");
            mockTranslator = new Mock<Translator>();
            mockTranslator.Setup(x => x.Languages).Returns(new List<Language> {supportedLanguage});
        }

        private Mock<Translator> mockTranslator;
        private Language supportedLanguage;
        private Language unsupportedLanguage;
        private Translator Translator => mockTranslator.Object;

        [Test]
        public void Setting_SourceLanguage_SetsLanguage_IfLanguageIsIncluded()
        {
            Translator.SourceLanguage = supportedLanguage;

            Assert.AreEqual(Translator.SourceLanguage, supportedLanguage);
        }

        [Test]
        public void Setting_SourceLanguage_SetsNull_IfNotIncluded()
        {
            Translator.SourceLanguage = unsupportedLanguage;
            Assert.IsNull(Translator.SourceLanguage);
        }

        [Test]
        public void Setting_TargetLanguage_SetsLanguage_IfLanguageIsIncluded()
        {
            Translator.TargetLanguage = supportedLanguage;

            Assert.AreEqual(Translator.TargetLanguage, supportedLanguage);
        }

        [Test]
        public void Setting_TargetLanguage_SetsNull_IfNotIncluded()
        {
            Translator.TargetLanguage = unsupportedLanguage;
            Assert.IsNull(Translator.TargetLanguage);
        }

        [Test]
        public async void TranslateAsync_SetsNonZeroTimeSpan_ForTranslation()
        {
            Translator.SourceLanguage = supportedLanguage;
            Translator.TargetLanguage = supportedLanguage;

            await Translator.TranslateAsync("Test Text");

            Assert.AreNotEqual(TimeSpan.Zero, Translator.TranslationTime);
        }
    }
}