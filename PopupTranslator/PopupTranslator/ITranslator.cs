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

        /// <summary>
        /// The text to translate's original language.
        /// </summary>
        Language SourceLanguage { get; set; }

        /// <summary>
        /// The language to translate the text to.
        /// </summary>
        Language TargetLanguage { get; set; }

        /// <summary>
        /// Translate between two languages in async.
        /// </summary>
        /// <param name="textToTranslate">The text to translate.</param>
        /// <returns></returns>
        Task<Translation> TranslateAsync(string textToTranslate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="languageName"></param>
        /// <returns></returns>
        Language LanguageNameToSupportedLanguage(string languageName);
    }
}