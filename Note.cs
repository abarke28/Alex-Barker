using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NotesApp.Model
{
    public class Note : INotifyPropertyChanged
    {

        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value) return;
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        private int _notebookId;
        [Indexed]
        public int NotebookId
        {
            get { return _notebookId; }
            set
            {
                if (_notebookId == value) return;
                _notebookId = value;
                OnPropertyChanged("NotebookId");
            }
        }


        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged("Title");
            }
        }


        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content == value) return;
                _content = value;
                OnPropertyChanged("Content");
            }
        }


        private int _noteLength;
        public int NoteLength
        {
            get { return _noteLength; }
            set
            {
                if (_noteLength == value) return;
                _noteLength = value;
                OnPropertyChanged("NoteLength");
            }
        }

        private DateTime _createdTime;
        public DateTime CreatedTime
        {
            get { return _createdTime; }
            set
            {
                if (_createdTime == value) return;
                _createdTime = value;
                OnPropertyChanged("CreatedTime");
            }
        }


        private DateTime _updatedTime;
        public DateTime UpdatedTime
        {
            get { return _updatedTime; }
            set
            {
                if (_updatedTime == value) return;
                _updatedTime = value;
                OnPropertyChanged("UpdatedTime");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
