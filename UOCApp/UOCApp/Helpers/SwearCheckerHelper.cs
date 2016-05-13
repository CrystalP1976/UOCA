using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Reflection;

namespace UOCApp.Helpers
{
    public class SwearCheckerHelper
    {
        private List<string> swearList;

        public SwearCheckerHelper()
        {
            //Console.WriteLine("Loading swear list!");

            //load the list
            loadSwearList();

        }

        public bool IsSwear(string name)
        {
            string[] words = name.Split(null);

            foreach (string refstr in swearList)
            {
                foreach (string word in words)
                {
                    if (String.Equals(word.ToLower(), refstr))
                        return true;
                }
            }

            return false;
        }

        private void loadSwearList()
        {
            swearList = new List<string>();

            //platform-specific 
#if __IOS__
                var resourcePrefix = "UOCApp.iOS.";
#endif
#if __ANDROID__
                var resourcePrefix = "UOCApp.Droid.";
#endif
#if WINDOWS_PHONE
                var resourcePrefix = "UOCApp.WinPhone.";
#endif

            //Console.WriteLine("Using this resource prefix: " + resourcePrefix.ToString());


            var assembly = typeof(App).GetTypeInfo().Assembly;
            //foreach (var res in assembly.GetManifestResourceNames())
            //    System.Diagnostics.Debug.WriteLine("found resource: " + res);

            Stream stream = assembly.GetManifestResourceStream(resourcePrefix + "terms.txt");

            //Stream fs = File.OpenRead(@"E:\Dropbox\ACIT 4900 - Group 7\terms.txt");
            StreamReader reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                swearList.Add(reader.ReadLine());
            }
            
            //swearList = list;
            
        }



    }
}
