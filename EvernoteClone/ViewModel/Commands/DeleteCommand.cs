using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteCommand : ICommand
    {
        private readonly NotesVM _vM;

        public event EventHandler CanExecuteChanged;

        public DeleteCommand(NotesVM vM)
        {
            _vM = vM;
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
                    _vM.DeleteNotebook(parameter as Notebook);
                }
                else if (parameter is Note)
                {
                    _vM.DeleteNote(parameter as Note);
                }
            }
        }
    }
}
