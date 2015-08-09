using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PopupTranslator.Annotations;

namespace PopupTranslator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string textToTranslate;
        private readonly Translator translator = new Translator();
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
            TranslatedText = await translator.TranslateAsync(textToTranslate, "English", "Chinese");
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}