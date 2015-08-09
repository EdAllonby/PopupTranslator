using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITranslator googleTranslator;
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

        public ICommand TranslateCommand => new RelayCommand(Translate, CanTranslate);

        public event PropertyChangedEventHandler PropertyChanged;

        private static bool CanTranslate(object obj)
        {
            return true;
        }

        public async void Translate(object obj)
        {
            var englishLanguage = googleTranslator.Languages.FirstOrDefault(x => x.Name.Equals("English"));
            var chineseLanguage = googleTranslator.Languages.FirstOrDefault(x => x.Name.Equals("Chinese"));

            TranslatedText = await googleTranslator.TranslateAsync(textToTranslate, englishLanguage, chineseLanguage);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}