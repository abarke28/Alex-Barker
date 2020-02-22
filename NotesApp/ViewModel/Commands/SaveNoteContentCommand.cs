using NotesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class SaveNoteContentCommand : ICommand
    {
        public NotesVM VM { get; set; }
        public SaveNoteContentCommand(NotesVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            Note selectedNote = parameter as Note;

            return selectedNote != null;
        }

        public void Execute(object parameter)
        {
            VM.SaveNoteContent();
        }
    }
}
