using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using System.IO;
using MyFantasticApp.Droid.Services;
using MyFantasticApp.Services;
using MyFantasticApp.Models;
using Android.Database.Sqlite;

[assembly: Dependency(typeof(SQLiteImplementation))]
namespace MyFantasticApp.Droid.Services
{
    public class SQLiteImplementation : ISQLite
    {
        public SQLiteImplementation() {}

        #region ISQLite implementation
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TodoSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            /*
            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                var s = Forms.Context.Resources.OpenRawResource(Resource.Raw.TripLogSQLite);  // RESOURCE NAME ###

                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                _readWriteStream(s, writeStream);
            }*/

            var conn = new SQLite.SQLiteConnection(path);
            /*
            try
            {
                conn.CreateTable<TripLogEntry>();
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            //*/

            return conn;
        }
        #endregion

        /// <summary>
        /// helper method to get the database out of /raw/ and into the user filesystem
        /// </summary>
        private void _readWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}