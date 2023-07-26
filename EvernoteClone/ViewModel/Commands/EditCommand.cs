using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EditCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesVM VM { get; set; }

        public EditCommand(NotesVM vM)
        {
            VM = vM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter != null)
            {
                if (parameter is Notebook)
                {
                    VM.StartEditingNotebook();
                }
                else if (parameter is Note)
                {
                    VM.StartEditingNote();
                }
            }
        }
    }
}
