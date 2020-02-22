using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.Model
{
    public class User : INotifyPropertyChanged
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


        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }


        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }


        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value) return;
                _username = value;
                OnPropertyChanged("Username");
            }
        }


        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("Email");
            }
        }


        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
