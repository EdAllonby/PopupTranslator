using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string textToTranslate;
        private readonly GoogleTranslator googleTranslator = new GoogleTranslator();
        private string translatedText;

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
            Language englishLanguage = googleTranslator.Languages.FirstOrDefault(x => x.Name.Equals("English"));
            Language chineseLanguage = googleTranslator.Languages.FirstOrDefault(x => x.Name.Equals("Chinese"));

            TranslatedText = await googleTranslator.TranslateAsync(textToTranslate, englishLanguage, chineseLanguage);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}