namespace PopupTranslator
{
    public class Translation
    {
        /// <summary>
        /// A translation with no optional phonetics.
        /// </summary>
        /// <param name="translatedText">The translated text.</param>
        public Translation(string translatedText) : this(translatedText, string.Empty)
        {
        }

        /// <summary>
        /// A translation with optional phonetics.
        /// </summary>
        /// <param name="translatedText">The translated text.</param>
        /// <param name="optionalPhonetics">The translated text's phonetics.</param>
        public Translation(string translatedText, string optionalPhonetics)
        {
            TranslatedText = translatedText;
            OptionalPhonetics = optionalPhonetics;
        }

        public string TranslatedText { get; }
        public string OptionalPhonetics { get; }

        /// <summary>
        /// An empty translation, safer than passing around null.
        /// </summary>
        /// <returns></returns>
        public static Translation EmptyTranslation()
        {
            return new Translation(string.Empty, string.Empty);
        }
    }
}