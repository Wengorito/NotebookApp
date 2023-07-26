using EvernoteClone.ViewModel;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace EvernoteClone.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private LoginVM _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            _viewModel = Resources["vm"] as LoginVM;
            _viewModel.Authenticated += ViewModel_Authenticated;
        }

        private void ViewModel_Authenticated(object sender, EventArgs e)
        {
            Close();
        }

        //https://stackoverflow.com/questions/13361260/how-to-distinguish-window-close-button-clicked-x-vs-window-close-in-closi
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            bool wasCodeClosed = new StackTrace().GetFrames().FirstOrDefault(x => x.GetMethod() == typeof(Window).GetMethod("Close")) != null;
            if (!wasCodeClosed)
            {
                Application.Current.Shutdown();
            }

            base.OnClosing(e);
        }
    }
}
