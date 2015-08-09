using System.Windows.Input;

namespace PopupTranslator
{
    internal class ContextItemViewModel
    {
        public ContextItemViewModel(string name, ICommand command)
        {
            Name = name;
            Action = command;
        }

        public string Name { get; }
        public ICommand Action { get; }
    }
}