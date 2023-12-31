﻿using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class EndEditingCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public NotesVM VM { get; set; }

        public EndEditingCommand(NotesVM vM)
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
                    VM.StopEditingNotebook(parameter as Notebook);

                }
                else if (parameter is Note)
                {
                    VM.StopEditingNote(parameter as Note);
                }
            }
        }
    }
}
