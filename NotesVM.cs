using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        //Properties
        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook _selectedNotebook;
        public Notebook SelectedNotebook
        {
            get { return _selectedNotebook; }
            set
            {
                _selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                NewNoteCommand.OnCanExecuteChanged();
                RenameNotebookCommand.OnCanExecuteChanged();
                DeleteNotebookCommand.OnCanExecuteChanged();

                if (_selectedNotebook != null) ReadNotes();
            }
        }

        private Note _selectedNote;
        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                OnPropertyChanged("SelectedNote");
                RenameNoteCommand.OnCanExecuteChanged();
                DeleteNoteCommand.OnCanExecuteChanged();
                SaveNoteContentCommand.OnCanExecuteChanged();

                if (_selectedNote != null) ReadNoteContent();
            }
        }
        private User _currentUser;
        public User CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }
        public ObservableCollection<Note> Notes { get; set; }

        //Commands
        public NewNoteCommand NewNoteCommand { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public ExitCommand ExitCommand { get; set; }
        public DeleteNoteCommand DeleteNoteCommand { get; set; }
        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }
        public RenameNoteCommand RenameNoteCommand { get; set; }
        public RenameNotebookCommand RenameNotebookCommand { get; set; }
        public SaveNoteContentCommand SaveNoteContentCommand { get; set; }
        public NotesVM()
        {
            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            ExitCommand = new ExitCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);
            RenameNoteCommand = new RenameNoteCommand(this);
            RenameNotebookCommand = new RenameNotebookCommand(this);
            SaveNoteContentCommand = new SaveNoteContentCommand(this);

            Notes = new ObservableCollection<Note>();
            Notebooks = new ObservableCollection<Notebook>();

            //TODO: Implement genuine login functionality. 
            CurrentUser = new User()
            {
                Id = 1,
                FirstName = "Alex",
                LastName = "Barker",
                Username = "Abarks",
                Email = "abarke28@gmail.com",
                Password = "guest"
            };

            ReadNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            var title = Microsoft.VisualBasic.Interaction.InputBox("Please enter Note Title", "Create New Note", "New Note");

            Note note = new Note()
            {
                NotebookId = notebookId,
                CreatedTime = DateTime.Now,
                UpdatedTime = DateTime.Now,
                Title = title
            };
            DatabaseHelper.Insert(note);

            ReadNotes();
        }

        public void CreateNotebook(User user)
        {
            //TODO: Implement name - explore using Popup instead of VB extension
            var name = Microsoft.VisualBasic.Interaction.InputBox("Please Enter Notebook Title", "Create New Notebook", "Notebook");

            Notebook notebook = new Notebook()
            {
                UserId = user.Id,
                Name = name
            };
            DatabaseHelper.Insert(notebook);

            ReadNotebooks();
        }

        public void ReadNotebooks()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Notebook>();
                var notebooks = conn.Table<Notebook>().ToList();

                //Clear so we do not re add everything every time
                Notebooks.Clear();

                foreach (Notebook notebook in notebooks)
                {
                    Notebooks.Add(notebook);
                }
            }
        }

        public void ReadNotes()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();

                if (SelectedNotebook != null)
                {
                    var notes = conn.Table<Note>().ToList().Where(n => n.NotebookId == SelectedNotebook.Id).ToList();

                    Notes.Clear();

                    foreach (Note note in notes)
                    {
                        Notes.Add(note);
                    }
                }
            }
        }
        
        public void ReadNoteContent()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();

                if (SelectedNote != null)
                {
                    SelectedNote.Content = conn.Table<Note>().ToList().Where(n => n.Id == SelectedNote.Id).ToList().First().Content;
                }
            }
        }

        public void SaveNoteContent()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();

                if (SelectedNote != null)
                {
                    DatabaseHelper.Update<Note>(SelectedNote);
                }
            }
        }
        public void RenameNote()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();

                if (SelectedNote != null)
                {
                    var title = Microsoft.VisualBasic.Interaction.InputBox("Please Enter New Note Title", "Rename Note", SelectedNote.Title);
                    SelectedNote.Title = title;
                    DatabaseHelper.Update(SelectedNote);
                }
                
            }

            ReadNotes();
        }

        public void RenameNotebook()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Notebook>();

                if (SelectedNotebook != null)
                {
                    var name = Microsoft.VisualBasic.Interaction.InputBox("Please Enter New Notebook Title", "Rename Notebook", SelectedNotebook.Name);
                    SelectedNotebook.Name = name;
                    DatabaseHelper.Update(SelectedNotebook);
                }
            }

            ReadNotebooks();
        }

        public void DeleteNote()
        {
            using(SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Note>();

                if (SelectedNote != null)
                {
                    DatabaseHelper.Delete(SelectedNote);
                }
            }

            ReadNotes();
        }

        public void DeleteNotebook()
        {

            //TODO delete all notes in notebook before deleting notebook
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<Notebook>();

                if (SelectedNotebook != null)
                {

                    //foreach (Note note in SelectedNotebook)

                    DatabaseHelper.Delete(SelectedNotebook);
                }
            }

            ReadNotebooks();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
