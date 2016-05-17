using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UOCApp.Models;

namespace UOCApp.Helpers
{
    public class DatabaseHelper
    {

        

        public SQLiteConnection db { get; private set; }

        public DatabaseHelper()
        {
            

            copyDatabase();
            connectDatabase();

            //tryDatabase();
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



            //Stream fs = File.OpenRead(@"E:\Dropbox\ACIT 4900 - Group 7\terms.txt");
            //StreamReader reader = new StreamReader(stream);

            //Console.WriteLine(reader.ReadToEnd());

            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "db.sqlite");

            //This checks if the database already exists before copying the embedded one to the storage location
            //remove this for production or the database will not be persistent
            
            if(File.Exists(filePath))
            {
                Console.WriteLine("DB already there!");
                File.Delete(filePath); //for debugging only
                //return;
            }            

            Console.WriteLine("Copying database to folder");

            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "db.sqlite");

            var fileStream = File.Create(filePath);

            stream.CopyTo(fileStream);

            fileStream.Close();
            stream.Close();

        }

        private void connectDatabase()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "db.sqlite");
            db = new SQLiteConnection(filePath);
        }

        private void tryDatabase()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, "db.sqlite");
            var db = new SQLiteConnection(filePath);
            this.db = db;

            foreach(var line in db.GetTableInfo("result"))
            {
                Console.WriteLine(line.ToString());
            }

            int pk = db.Insert(new Result { date = "2016-12-12", ranked = 1, time = 240.123m, student_gender = "F", student_name = "Jamie Tang", student_grade = 4 });

            Console.WriteLine("Inserted row with PK of " + pk);

            var query = db.Table<Result>();

            Console.WriteLine(query);
            Console.WriteLine(query.Count());

            foreach(var result in query)
            {
                Console.WriteLine(result.ToString());
            }

            //Console.WriteLine(db.GetTableInfo("result").ToString());
            //db.Insert(new Result {date="2016-12-12",ranked=1,time=240.123m, student_gender="M",student_name="Jamie Tang",student_grade=4 });
        }

        //convert db results to private results
        public List<PrivateResult> GetPrivateResults()
        {
            List<PrivateResult> results = new List<PrivateResult>();

            //this is going to be slow and could be optimized
            foreach (Result result in db.Table<Result>())
            {
                List<string> obstaclesList = null;
                bool missedObstacle;

                var bridgeQuery = db.Table<ResultObstacle>().Where(v => v.result_id == result.result_id);

                if(bridgeQuery.Count() != 0)
                {
                    obstaclesList = new List<string>();

                    foreach(ResultObstacle ro in bridgeQuery)
                    {
                        var obstaclesQuery = db.Table<Obstacle>().Where(v => v.obstacle_id == ro.obstacle_id);
                        obstaclesList.Add(obstaclesQuery.First().obstacle_name);
                    }
                    
                }

                results.Add(new PrivateResult(result,obstaclesList));
            }

            return results;
        }

        /*
        public List<Result> GetPrivateResults()
        {
            //throw new NotImplementedException();
            //var query = db.Table<Result>();
            return new List<Result>(db.Table<Result>());
        }
        */

    }
}
