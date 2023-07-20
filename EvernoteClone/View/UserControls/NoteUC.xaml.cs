using EvernoteClone.Model;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.View.UserControls
{
    /// <summary>
    /// Interaction logic for NoteUC.xaml
    /// </summary>
    public partial class NoteUC : UserControl
    {
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Notebook.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(NoteUC), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var noteUserControl = d as NoteUC;

            if (noteUserControl != null)
            {
                noteUserControl.DataContext = noteUserControl.Note;
            }
        }
        public NoteUC()
        {
            InitializeComponent();
        }
    }
}
