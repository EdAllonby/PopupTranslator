using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PopupTranslator
{
    /// <summary>
    /// Translates text using Google's online language tools.
    /// </summary>
    public class GoogleTranslator : ITranslator
    {
        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        /// <summary>
        /// The language to translation mode map.
        /// </summary>
        private static List<Language> languages;

        /// <summary>
        /// Gets the url used to speak the translation.
        /// </summary>
        /// <value>The url used to speak the translation.</value>
        public string TranslationSpeechUrl { get; private set; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets the time taken to perform the translation.
        /// </summary>
        public TimeSpan TranslationTime { get; private set; }

        /// <summary>
        /// Gets the supported languages.
        /// </summary>
        public IEnumerable<Language> Languages
        {
            get
            {
                EnsureInitialized();
                return languages;
            }
        }

        /// <summary>
        /// Translates the specified source text.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="sourceLanguage">The source language.</param>
        /// <param name="targetLanguage">The target language.</param>
        /// <returns>The translation.</returns>
        public async Task<string> TranslateAsync(string sourceText, Language sourceLanguage, Language targetLanguage)
        {
            ResetState();
            var tmStart = DateTime.Now;
            var translation = string.Empty;

            try
            {
                var outputFile = await SendTranslationRequestAsync(sourceText, sourceLanguage, targetLanguage);
                translation = ParseTranslationHtml(sourceLanguage, targetLanguage, outputFile, translation);
            }
            catch (Exception exception)
            {
                Error = exception;
            }

            // Return result
            TranslationTime = DateTime.Now - tmStart;
            return translation;
        }

        private void ResetState()
        {
            Error = null;
            TranslationSpeechUrl = null;
            TranslationTime = TimeSpan.Zero;
        }

        private static async Task<string> SendTranslationRequestAsync(string sourceText, Language sourceLanguage, Language targetLanguage)
        {
            var requestUrl = CreateRequestUrl(sourceText, sourceLanguage, targetLanguage);

            var outputFile = Path.GetTempFileName();

            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("user-agent", UserAgent);
                await webClient.DownloadFileTaskAsync(requestUrl, outputFile);
            }

            return outputFile;
        }

        private static string CreateRequestUrl(string sourceText, Language sourceLanguage, Language targetLanguage)
        {
            return $"https://translate.google.com/translate_a/single?client=t&sl={sourceLanguage.Identifier}&tl={targetLanguage.Identifier}&hl=en&dt=bd&dt=ex&dt=ld&dt=md&dt=qc&dt=rw&dt=rm&dt=ss&dt=t&dt=at&ie=UTF-8&oe=UTF-8&source=btn&ssel=0&tsel=0&kc=0&q={HttpUtility.UrlEncode(sourceText)}";
        }

        private string ParseTranslationHtml(Language sourceLanguage, Language targetLanguage, string outputFile, string translation)
        {
            if (File.Exists(outputFile))
            {
                // Get phrase collection
                var text = File.ReadAllText(outputFile);
                var index = text.IndexOf($",,\"{sourceLanguage.Identifier}\"", StringComparison.Ordinal);
                if (index == -1)
                {
                    // Translation of single word
                    var startQuote = text.IndexOf('\"');
                    if (startQuote != -1)
                    {
                        var endQuote = text.IndexOf('\"', startQuote + 1);
                        if (endQuote != -1)
                        {
                            translation = text.Substring(startQuote + 1, endQuote - startQuote - 1);
                        }
                    }
                }
                else
                {
                    // Translation of phrase
                    text = text.Substring(0, index);
                    text = text.Replace("],[", ",");
                    text = text.Replace("]", string.Empty);
                    text = text.Replace("[", string.Empty);
                    text = text.Replace("\",\"", "\"");

                    // Get translated phrases
                    var phrases = text.Split(new[] {'\"'}, StringSplitOptions.RemoveEmptyEntries);
                    for (var i = 0; (i < phrases.Length); i += 2)
                    {
                        var translatedPhrase = phrases[i];
                        if (translatedPhrase.StartsWith(",,"))
                        {
                            i--;
                            continue;
                        }
                        translation += translatedPhrase + "  ";
                    }
                }

                // Fix up translation
                translation = translation.Trim();
                translation = translation.Replace(" ?", "?");
                translation = translation.Replace(" !", "!");
                translation = translation.Replace(" ,", ",");
                translation = translation.Replace(" .", ".");
                translation = translation.Replace(" ;", ";");

                // And translation speech URL
                TranslationSpeechUrl = $"https://translate.google.com/translate_tts?ie=UTF-8&q={HttpUtility.UrlEncode(translation)}&tl={targetLanguage.Identifier}&total=1&idx=0&textlen={translation.Length}&client=t";
            }

            return translation;
        }

        /// <summary>
        /// Ensures the translator has been initialized.
        /// </summary>
        private static void EnsureInitialized()
        {
            if (languages == null)
            {
                SupportedLanguages();
            }
        }

        private static void SupportedLanguages()
        {
            languages = new List<Language>();
            languages.Add(new Language("Afrikaans", "af"));
            languages.Add(new Language("Albanian", "sq"));
            languages.Add(new Language("Arabic", "ar"));
            languages.Add(new Language("Armenian", "hy"));
            languages.Add(new Language("Azerbaijani", "az"));
            languages.Add(new Language("Basque", "eu"));
            languages.Add(new Language("Belarusian", "be"));
            languages.Add(new Language("Bengali", "bn"));
            languages.Add(new Language("Bulgarian", "bg"));
            languages.Add(new Language("Catalan", "ca"));
            languages.Add(new Language("Chinese", "zh-CN"));
            languages.Add(new Language("Croatian", "hr"));
            languages.Add(new Language("Czech", "cs"));
            languages.Add(new Language("Danish", "da"));
            languages.Add(new Language("Dutch", "nl"));
            languages.Add(new Language("English", "en"));
            languages.Add(new Language("Esperanto", "eo"));
            languages.Add(new Language("Estonian", "et"));
            languages.Add(new Language("Filipino", "tl"));
            languages.Add(new Language("Finnish", "fi"));
            languages.Add(new Language("French", "fr"));
            languages.Add(new Language("Galician", "gl"));
            languages.Add(new Language("German", "de"));
            languages.Add(new Language("Georgian", "ka"));
            languages.Add(new Language("Greek", "el"));
            languages.Add(new Language("Haitian Creole", "ht"));
            languages.Add(new Language("Hebrew", "iw"));
            languages.Add(new Language("Hindi", "hi"));
            languages.Add(new Language("Hungarian", "hu"));
            languages.Add(new Language("Icelandic", "is"));
            languages.Add(new Language("Indonesian", "id"));
            languages.Add(new Language("Irish", "ga"));
            languages.Add(new Language("Italian", "it"));
            languages.Add(new Language("Japanese", "ja"));
            languages.Add(new Language("Korean", "ko"));
            languages.Add(new Language("Lao", "lo"));
            languages.Add(new Language("Latin", "la"));
            languages.Add(new Language("Latvian", "lv"));
            languages.Add(new Language("Lithuanian", "lt"));
            languages.Add(new Language("Macedonian", "mk"));
            languages.Add(new Language("Malay", "ms"));
            languages.Add(new Language("Maltese", "mt"));
            languages.Add(new Language("Norwegian", "no"));
            languages.Add(new Language("Persian", "fa"));
            languages.Add(new Language("Polish", "pl"));
            languages.Add(new Language("Portuguese", "pt"));
            languages.Add(new Language("Romanian", "ro"));
            languages.Add(new Language("Russian", "ru"));
            languages.Add(new Language("Serbian", "sr"));
            languages.Add(new Language("Slovak", "sk"));
            languages.Add(new Language("Slovenian", "sl"));
            languages.Add(new Language("Spanish", "es"));
            languages.Add(new Language("Swahili", "sw"));
            languages.Add(new Language("Swedish", "sv"));
            languages.Add(new Language("Tamil", "ta"));
            languages.Add(new Language("Telugu", "te"));
            languages.Add(new Language("Thai", "th"));
            languages.Add(new Language("Turkish", "tr"));
            languages.Add(new Language("Ukrainian", "uk"));
            languages.Add(new Language("Urdu", "ur"));
            languages.Add(new Language("Vietnamese", "vi"));
            languages.Add(new Language("Welsh", "cy"));
            languages.Add(new Language("Yiddish", "yi"));
        }
    }
}