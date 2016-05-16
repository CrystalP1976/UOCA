using System;

namespace UOCApp
{
	public class ResultDB
	{
		public ResultDB ()
		{
		}

		string DatabasePath {
			get {
				var sqliteFilename = "Times.sqlite";
				#if __IOS__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				string libraryPath = Path.Combine (documentsPath, "..", "Library"); // Library folder
				var path = Path.Combine(libraryPath, sqliteFilename);
				#else
				#if __ANDROID__
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
				var path = Path.Combine(documentsPath, sqliteFilename);
				#else
				// WinPhone
				var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);;
				#endif
				#endif
				return path;
			}
		}

		public IEnumerable<TodoItem> GetItems () {
			return (from i in database.Table<TodoItem>() select i).ToList();
		}
		public IEnumerable<TodoItem> GetItemsNotDone ()
		{
			return database.Query<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
		}
		public TodoItem GetItem (int id)
		{
			return database.Table<TodoItem>().FirstOrDefault(x => x.ID == id);
		}
		public int DeleteItem(int id)
		{
			return database.Delete<TodoItem>(id);
		}

	}
}

