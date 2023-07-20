using EvernoteClone.Model;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.View.UserControls
{
    /// <summary>
    /// Interaction logic for NotebookUC.xaml
    /// </summary>
    public partial class NotebookUC : UserControl
    {


        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Notebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(NotebookUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var notebookUserControl = d as NotebookUC;

            if (notebookUserControl != null)
            {
                notebookUserControl.DataContext = notebookUserControl.Notebook;
            }
        }

        public NotebookUC()
        {
            InitializeComponent();
        }
    }
}
