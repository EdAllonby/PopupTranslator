namespace PopupTranslator
{
    public class Translation
    {
        public Translation(string translatedText, string optionalPhonetics)
        {
            TranslatedText = translatedText;
            OptionalPhonetics = optionalPhonetics;
        }

        public string TranslatedText { get; }
        public string OptionalPhonetics { get; }

        public static Translation EmptyTranslation()
        {
            return new Translation(string.Empty, string.Empty);
        }
    }
}