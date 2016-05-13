using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace UOCApp.Helpers
{
    public class DatabaseHelper
    {
        public DatabaseHelper()
        {


            copyDatabase();

            tryDatabase();
        }

        private void copyDatabase()
        {

            //platform - specific
#if __IOS__
                var resourcePrefix = "UOCApp.iOS.";
#endif
#if __ANDROID__
                var resourcePrefix = "UOCApp.Droid.";
#endif
#if WINDOWS_PHONE
                var resourcePrefix = "UOCApp.WinPhone.";
#endif

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "db.sqlite");

            //Stream fs = File.OpenRead(@"E:\Dropbox\ACIT 4900 - Group 7\terms.txt");
            //StreamReader reader = new StreamReader(stream);

            //Console.WriteLine(reader.ReadToEnd());

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "db.sqlite");

            if(File.Exists(filePath))
            {
                Console.WriteLine("DB already there!");
                return;
            }

            var fileStream = File.Create(filePath);

            stream.CopyTo(fileStream);

            fileStream.Close();
            stream.Close();

        }

        private void tryDatabase()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "db.sqlite");
            var db = new SQLiteConnection(filePath);

            foreach(var line in db.GetTableInfo("result"))
            {
                Console.WriteLine(line.ToString());
            }

            //Console.WriteLine(db.GetTableInfo("result").ToString());
        }

    }
}
