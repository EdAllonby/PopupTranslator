using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PopupTranslator
{
    /// <summary>
    /// Translates text using Google's online language tools.
    /// </summary>
    public class GoogleTranslator : Translator
    {
        private const string UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";

        /// <summary>
        /// The language to translation mode map.
        /// </summary>
        private static IList<Language> supportedLanguages;

        public GoogleTranslator()
        {
            SourceLanguage = LanguageNameToSupportedLanguage("English");
            TargetLanguage = LanguageNameToSupportedLanguage("Chinese");
        }

        /// <summary>
        /// Gets the url used to speak the translation.
        /// </summary>
        /// <value>The url used to speak the translation.</value>
        public string TranslationSpeechUrl { get; private set; }

        /// <summary>
        /// Gets the supported languages.
        /// </summary>
        public override IEnumerable<Language> Languages
        {
            get
            {
                EnsureInitialized();
                return supportedLanguages;
            }
        }

        /// <summary>
        /// Translates the specified source text.
        /// </summary>
        /// <param name="textToTranslate">The text to translate.</param>
        /// <returns>The translation.</returns>
        protected override async Task<Translation> TranslateTextAsync(string textToTranslate)
        {
            string outputFile = await SendTranslationRequestAsync(textToTranslate, SourceLanguage, TargetLanguage);
            return ParseTranslationHtml(textToTranslate.Trim(), SourceLanguage, TargetLanguage, outputFile);
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

        private static string CreateRequestUrl(string googleText, Language sourceLanguage, Language targetLanguage)
        {
            return $"https://translate.google.com/translate_a/single?client=t&sl={sourceLanguage.Identifier}&tl={targetLanguage.Identifier}&hl=en&dt=bd&dt=ex&dt=ld&dt=md&dt=qc&dt=rw&dt=rm&dt=ss&dt=t&dt=at&ie=UTF-8&oe=UTF-8&source=btn&ssel=0&tsel=0&kc=0&q={HttpUtility.UrlEncode(googleText)}";
        }

        private Translation ParseTranslationHtml(string originalText, Language sourceLanguage, Language targetLanguage, string outputFile)
        {
            if (!File.Exists(outputFile))
            {
                return Translation.EmptyTranslation();
            }

            // Get phrase collection
            var text = File.ReadAllText(outputFile);
            var index = text.IndexOf($",,\"{sourceLanguage.Identifier}\"", StringComparison.Ordinal);

            if (index == -1)
            {
                return TranslateSingleWord(text);
            }

            text = text.Substring(0, index);
            return TranslatePhrases(originalText, text);

            // And translation speech URL
            // TranslationSpeechUrl = $"https://translate.google.com/translate_tts?ie=UTF-8&q={HttpUtility.UrlEncode(translation)}&tl={targetLanguage.Identifier}&total=1&idx=0&textlen={translation.Length}&client=t";
        }

        private static Translation TranslateSingleWord(string text)
        {
            int startQuote = text.IndexOf('\"');

            if (startQuote == -1)
            {
                return Translation.EmptyTranslation();
            }

            int endQuote = text.IndexOf('\"', startQuote + 1);

            if (endQuote == -1)
            {
                return Translation.EmptyTranslation();
            }

            string cleanedTranslation = CleanTranslationText(text.Substring(startQuote + 1, endQuote - startQuote - 1));
            return new Translation(cleanedTranslation);
        }

        private static string CleanTranslationText(string translationText)
        {
            translationText = translationText.Trim();
            translationText = translationText.Replace(" ?", "?");
            translationText = translationText.Replace(" !", "!");
            translationText = translationText.Replace(" ,", ",");
            translationText = translationText.Replace(" .", ".");
            translationText = translationText.Replace(" ;", ";");

            return translationText;
        }

        private static Translation TranslatePhrases(string originalText, string googleText)
        {
            googleText = googleText.Replace("],[", ",");
            googleText = googleText.Replace("]", string.Empty);
            googleText = googleText.Replace("[", string.Empty);
            googleText = googleText.Replace("\",\"", "\"");

            // Get translated phrases
            IEnumerable<string> filteredPhrases = googleText.Split(new[] {'\"'}, StringSplitOptions.RemoveEmptyEntries).Where(x => !x.StartsWith(",,"));
            var translatedTextOptions = filteredPhrases.Where(x => !x.Equals(originalText)).ToList();

            if (!translatedTextOptions.Any())
            {
                return Translation.EmptyTranslation();
            }

            return translatedTextOptions.Count == 1
                ? new Translation(CleanTranslationText(translatedTextOptions.First()))
                : new Translation(CleanTranslationText(translatedTextOptions.First()), CleanTranslationText(translatedTextOptions.LastOrDefault()));
        }

        /// <summary>
        /// Ensures the translator has been initialized.
        /// </summary>
        private static void EnsureInitialized()
        {
            if (supportedLanguages == null)
            {
                SupportedLanguages();
            }
        }

        private static void SupportedLanguages()
        {
            supportedLanguages = new List<Language>();
            supportedLanguages.Add(new Language("Afrikaans", "af"));
            supportedLanguages.Add(new Language("Albanian", "sq"));
            supportedLanguages.Add(new Language("Arabic", "ar"));
            supportedLanguages.Add(new Language("Armenian", "hy"));
            supportedLanguages.Add(new Language("Azerbaijani", "az"));
            supportedLanguages.Add(new Language("Basque", "eu"));
            supportedLanguages.Add(new Language("Belarusian", "be"));
            supportedLanguages.Add(new Language("Bengali", "bn"));
            supportedLanguages.Add(new Language("Bulgarian", "bg"));
            supportedLanguages.Add(new Language("Catalan", "ca"));
            supportedLanguages.Add(new Language("Chinese", "zh-CN"));
            supportedLanguages.Add(new Language("Croatian", "hr"));
            supportedLanguages.Add(new Language("Czech", "cs"));
            supportedLanguages.Add(new Language("Danish", "da"));
            supportedLanguages.Add(new Language("Dutch", "nl"));
            supportedLanguages.Add(new Language("English", "en"));
            supportedLanguages.Add(new Language("Esperanto", "eo"));
            supportedLanguages.Add(new Language("Estonian", "et"));
            supportedLanguages.Add(new Language("Filipino", "tl"));
            supportedLanguages.Add(new Language("Finnish", "fi"));
            supportedLanguages.Add(new Language("French", "fr"));
            supportedLanguages.Add(new Language("Galician", "gl"));
            supportedLanguages.Add(new Language("German", "de"));
            supportedLanguages.Add(new Language("Georgian", "ka"));
            supportedLanguages.Add(new Language("Greek", "el"));
            supportedLanguages.Add(new Language("Haitian Creole", "ht"));
            supportedLanguages.Add(new Language("Hebrew", "iw"));
            supportedLanguages.Add(new Language("Hindi", "hi"));
            supportedLanguages.Add(new Language("Hungarian", "hu"));
            supportedLanguages.Add(new Language("Icelandic", "is"));
            supportedLanguages.Add(new Language("Indonesian", "id"));
            supportedLanguages.Add(new Language("Irish", "ga"));
            supportedLanguages.Add(new Language("Italian", "it"));
            supportedLanguages.Add(new Language("Japanese", "ja"));
            supportedLanguages.Add(new Language("Korean", "ko"));
            supportedLanguages.Add(new Language("Lao", "lo"));
            supportedLanguages.Add(new Language("Latin", "la"));
            supportedLanguages.Add(new Language("Latvian", "lv"));
            supportedLanguages.Add(new Language("Lithuanian", "lt"));
            supportedLanguages.Add(new Language("Macedonian", "mk"));
            supportedLanguages.Add(new Language("Malay", "ms"));
            supportedLanguages.Add(new Language("Maltese", "mt"));
            supportedLanguages.Add(new Language("Norwegian", "no"));
            supportedLanguages.Add(new Language("Persian", "fa"));
            supportedLanguages.Add(new Language("Polish", "pl"));
            supportedLanguages.Add(new Language("Portuguese", "pt"));
            supportedLanguages.Add(new Language("Romanian", "ro"));
            supportedLanguages.Add(new Language("Russian", "ru"));
            supportedLanguages.Add(new Language("Serbian", "sr"));
            supportedLanguages.Add(new Language("Slovak", "sk"));
            supportedLanguages.Add(new Language("Slovenian", "sl"));
            supportedLanguages.Add(new Language("Spanish", "es"));
            supportedLanguages.Add(new Language("Swahili", "sw"));
            supportedLanguages.Add(new Language("Swedish", "sv"));
            supportedLanguages.Add(new Language("Tamil", "ta"));
            supportedLanguages.Add(new Language("Telugu", "te"));
            supportedLanguages.Add(new Language("Thai", "th"));
            supportedLanguages.Add(new Language("Turkish", "tr"));
            supportedLanguages.Add(new Language("Ukrainian", "uk"));
            supportedLanguages.Add(new Language("Urdu", "ur"));
            supportedLanguages.Add(new Language("Vietnamese", "vi"));
            supportedLanguages.Add(new Language("Welsh", "cy"));
            supportedLanguages.Add(new Language("Yiddish", "yi"));
        }
    }
}