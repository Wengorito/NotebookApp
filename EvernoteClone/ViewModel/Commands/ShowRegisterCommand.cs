using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        private LoginVM _loginVM;

        public event EventHandler CanExecuteChanged;

        public ShowRegisterCommand(LoginVM loginVM)
        {
            _loginVM = loginVM;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _loginVM.SwitchViews();
        }
    }
}
