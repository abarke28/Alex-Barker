using NotesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotesApp.ViewModel.Commands
{
    public class RenameNotebookCommand : ICommand
    {
        public NotesVM VM { get; set; }

        public RenameNotebookCommand(NotesVM vm)
        {
            VM = vm;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            if (selectedNotebook != null) return true;

            return false;
        }

        public void Execute(object parameter)
        {
            VM.RenameNotebook();
        }
    }
}
