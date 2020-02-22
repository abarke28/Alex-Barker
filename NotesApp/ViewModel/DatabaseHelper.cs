using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NotesApp.ViewModel
{
    public class DatabaseHelper
    {
        public static readonly string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDB.db3");
        public static bool Insert<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                if (conn.Insert(item)>0) result = true;
            }

            return result;
        }
        public static bool Update<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                if (conn.Update(item) > 0) result = true;
            }

            return result;
        }
        public static bool Delete<T>(T item)
        {
            bool result = false;

            using (SQLiteConnection conn = new SQLiteConnection(dbFile))
            {
                conn.CreateTable<T>();
                if (conn.Delete(item) > 0) result = true;            }

            return result;
        }
    }
}
