﻿using SQLite;
using Rest1.Droid;
using System.IO;
[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]

namespace Rest1.Droid
{
  public class DatabaseConnection_Android : IDatabaseConnection
  {
    public SQLiteConnection DbConnection()
    {
      var dbName = "sacDb.db3";
      var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbName);

            return new SQLiteConnection(path);
    }
  }
}