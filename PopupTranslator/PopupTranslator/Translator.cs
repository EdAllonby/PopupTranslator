using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopupTranslator
{
    public abstract class Translator : ITranslator
    {
        private Language sourceLanguage;
        private Language targetLanguage;

        public abstract IEnumerable<Language> Languages { get; }

        public TimeSpan TranslationTime { get; private set; }

        public Language SourceLanguage
        {
            get { return sourceLanguage; }
            set
            {
                if (DoesTranslatorContainLanguage(value))
                {
                    sourceLanguage = value;
                }
            }
        }

        public Language TargetLanguage
        {
            get { return targetLanguage; }
            set
            {
                if (DoesTranslatorContainLanguage(value))
                {
                    targetLanguage = value;
                }
            }
        }

        public async Task<Translation> TranslateAsync(string textToTranslate)
        {
            TranslationTime = TimeSpan.Zero;
            var translationStartTime = DateTime.Now;

            Translation translation = await TranslateTextAsync(textToTranslate);

            TranslationTime = DateTime.Now - translationStartTime;

            return translation;
        }

        protected Language LanguageNameToSupportedLanguage(string languageName)
        {
            return Languages.FirstOrDefault(supportedLanguage => supportedLanguage.Name.Equals(languageName, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// The core implementation of the translator.
        /// </summary>
        /// <param name="textToTranslate">The text to translate.</param>
        /// <returns></returns>
        protected abstract Task<Translation> TranslateTextAsync(string textToTranslate);

        private bool DoesTranslatorContainLanguage(Language value)
        {
            return value != null && Languages.Contains(value);
        }
    }
}