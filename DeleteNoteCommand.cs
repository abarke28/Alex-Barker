using NotesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class DeleteNoteCommand : ICommand
    {
        public NotesVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public DeleteNoteCommand(NotesVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            Note selectedNote = parameter as Note;

            if (selectedNote != null) return true;
            return false;
        }
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            Note selectedNotebook = parameter as Note;

            if (selectedNotebook != null)
                VM.DeleteNote();
        }
    }
}
