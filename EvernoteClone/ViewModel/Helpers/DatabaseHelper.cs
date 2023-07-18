using SQLite;
using System;
using System.Collections.Generic;
using System.IO;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var rows = connection.Insert(item);

                if (rows > 0)
                    return true;

                return false;
            }
        }

        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            }

            return items;
        }

        public static bool Update<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var rows = connection.Update(item);

                if (rows > 0)
                    return true;

                return false;
            }
        }

        public static bool Delete<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var rows = connection.Delete(item);

                if (rows > 0)
                    return true;

                return false;
            }
        }

        public static bool DeleteAll<T>()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var rows = connection.DeleteAll<T>();

                if (rows > 0)
                    return true;

                return false;
            }
        }

        public static void DropTable<T>()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.DropTable<T>();
            }
        }
    }
}
