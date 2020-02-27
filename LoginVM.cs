using NotesApp.Model;
using NotesApp.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesApp.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private User _user;
        public User User
        {
            get { return _user; }
            set { _user = value; }
        }

        private string _loginScreenVisibility;
        public string LoginScreenVisibility
        {
            get { return _loginScreenVisibility; }
            set
            {
                if (_loginScreenVisibility == value) return;
                _loginScreenVisibility = value;
                OnPropertyChanged("LoginScreenVisibility");
            }
        }

        private string _registerScreenVisibility;
        public string RegisterScreenVisibility
        {
            get { return _registerScreenVisibility; }
            set
            {
                if (_registerScreenVisibility == value) return;
                _registerScreenVisibility = value;
                OnPropertyChanged("RegisterScreenVisibility");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public RegisterCommand RegisterCommand { get; set; }
        public LoginCommand LoginCommand { get; set; }
        public HaveAccountCommand HaveAccountCommand { get; set; }
        public DontHaveAccountCommand DontHaveAccountCommand { get; set; }

        public LoginVM()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            HaveAccountCommand = new HaveAccountCommand(this);
            DontHaveAccountCommand = new DontHaveAccountCommand(this);

            RegisterScreenVisibility = "Visible";
            LoginScreenVisibility = "Collapsed";
        }

        public void Login()
        {
            using(SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<User>();

                var user = conn.Table<User>().Where(u => u.Username == User.Username).FirstOrDefault();

                if (user.Password == User.Password)
                {
                    //TODO: Succussful Login
                }
            }
        }

        public void Register()
        {
            using(SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(DatabaseHelper.dbFile))
            {
                conn.CreateTable<User>();

                var result = DatabaseHelper.Insert<User>(User);

                if (result)
                {
                    //TODO: Register functionality
                }
            }
        }

        public void HaveAccount()
        {
            RegisterScreenVisibility = "Collapsed";
            LoginScreenVisibility = "Visible";
        }

        public void DontHaveAccount()
        {
            RegisterScreenVisibility = "Visible";
            LoginScreenVisibility = "Collapsed";
        }

    }
}
