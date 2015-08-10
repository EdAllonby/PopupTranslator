using System.Windows.Input;

namespace PopupTranslator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITranslator googleTranslator;
        private string optionalPhonetics;
        private string textToTranslate;
        private string translatedText;

        public MainViewModel(ITranslator translator)
        {
            googleTranslator = translator;
        }

        public string TextToTranslate
        {
            get { return textToTranslate; }
            set
            {
                textToTranslate = value;
                Translate(TextToTranslate);
                OnPropertyChanged();
            }
        }

        public string TranslatedText
        {
            get { return translatedText; }
            set
            {
                translatedText = value;
                OnPropertyChanged();
            }
        }

        public string OptionalPhonetics
        {
            get { return optionalPhonetics; }
            set
            {
                optionalPhonetics = value;
                OnPropertyChanged();
            }
        }

        public ICommand TranslateCommand => new RelayCommand(Translate, CanTranslate);

        private static bool CanTranslate(object obj)
        {
            return true;
        }

        public async void Translate(object obj)
        {
            Translation translation = await googleTranslator.TranslateAsync(textToTranslate);

            TranslatedText = translation.TranslatedText;
            OptionalPhonetics = translation.OptionalPhonetics;
        }
    }
}