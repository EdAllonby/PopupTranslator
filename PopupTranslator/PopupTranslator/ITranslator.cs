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
        /// Translate between two languages in async.
        /// </summary>
        /// <param name="sourceText">The text to translate.</param>
        /// <param name="sourceLanguage">The source language.</param>
        /// <param name="targetLanguage">The target language.</param>
        /// <returns></returns>
        Task<string> TranslateAsync(string sourceText, Language sourceLanguage, Language targetLanguage);
    }
}