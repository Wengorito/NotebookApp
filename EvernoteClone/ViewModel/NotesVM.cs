using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        private Notebook _selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set
            {
                _selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        private Visibility _isVisibleEditNotebook;
        public Visibility IsVisibleEditNotebook
        {
            get { return _isVisibleEditNotebook; }
            set
            {
                _isVisibleEditNotebook = value;
                OnPropertyChanged(nameof(IsVisibleEditNotebook));
            }
        }

        private Visibility _isVisibleEditNote;
        public Visibility IsVisibleEditNote
        {
            get { return _isVisibleEditNote; }
            set
            {
                _isVisibleEditNote = value;
                OnPropertyChanged(nameof(IsVisibleEditNote));
            }
        }

        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public EditCommand EditCommand { get; set; }
        public EndEditingCommand EndEditingCommand { get; set; }
        public DeleteCommand DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler SelectedNoteChanged;

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            DeleteCommand = new DeleteCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisibleEditNotebook = Visibility.Collapsed;
            IsVisibleEditNote = Visibility.Collapsed;

            GetNotebooks();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void CreateNote(string notebookId)
        {
            var newNote = new Note
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = $"New Note"
            };

            //DatabaseHelper.Insert(newNote);
            await FirebaseDatabaseHelper.Insert(newNote);

            GetNotes();
        }

        public async void CreateNotebook()
        {
            var newNotebook = new Notebook
            {
                Name = "New notebook",
                UserId = App.UserId
            };

            //DatabaseHelper.Insert(newNotebook);
            await FirebaseDatabaseHelper.Insert(newNotebook);

            GetNotebooks();
        }

        public async void GetNotebooks()
        {
            var notebooks = (await FirebaseDatabaseHelper.Read<Notebook>()).Where(n => n.UserId == App.UserId).ToList();

            Notebooks.Clear();
            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook != null)
            {
                var notes = (await FirebaseDatabaseHelper.Read<Note>()).Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
        }

        public void StartEditingNotebook()
        {
            IsVisibleEditNotebook = Visibility.Visible;
        }

        public void StartEditingNote()
        {
            IsVisibleEditNote = Visibility.Visible;
        }

        public async void StopEditingNotebook(Notebook notebook)
        {
            IsVisibleEditNotebook = Visibility.Collapsed;
            //DatabaseHelper.Update(notebook);
            await FirebaseDatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public async void StopEditingNote(Note note)
        {
            IsVisibleEditNote = Visibility.Collapsed;
            //DatabaseHelper.Update(note);
            await FirebaseDatabaseHelper.Update(note);
            GetNotebooks();
        }

        public async void DeleteNotebook(Notebook notebook)
        {
            var notes = (await FirebaseDatabaseHelper.Read<Note>()).Where(n => n.NotebookId == notebook.Id).ToList();
            foreach (var note in notes)
            {
                await FirebaseDatabaseHelper.Delete(note);
            }

            await FirebaseDatabaseHelper.Delete(notebook);
            Notes.Clear();
            GetNotebooks();
        }

        public async void DeleteNote(Note note)
        {
            await AzureStorageHelper.DeleteIfExistsFile($"{SelectedNote.Id}.rtf");

            await FirebaseDatabaseHelper.Delete(note);

            GetNotes();
        }
    }
}
