using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopupTranslator
{
    public interface ITranslator
    {
        /// <summary>
        /// The supported languages to translate.
        /// </summary>
        IEnumerable<Language> Languages { get; }

        /// <summary>
        /// The time it took to translate the last request.
        /// </summary>
        TimeSpan TranslationTime { get; }

        Language SourceLanguage { get; set; }

        Language TargetLanguage { get; set; }

        /// <summary>
        /// Translate between two languages in async.
        /// </summary>
        /// <param name="textToTranslate">The text to translate.</param>
        /// <returns></returns>
        Task<Translation> TranslateAsync(string textToTranslate);

        Language LanguageNameToSupportedLanguage(string languageName);
    }
}