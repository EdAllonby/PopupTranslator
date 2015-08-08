namespace PopupTranslator
{
    public class Language
    {
        public Language(string name, string identifier)
        {
            Name = name;
            Identifier = identifier;
        }

        public string Name { get; }
        public string Identifier { get; }
    }
}