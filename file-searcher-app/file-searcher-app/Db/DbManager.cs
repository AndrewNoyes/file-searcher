using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using file_searcher_app.Models;

namespace file_searcher_app.Db
{
    public class DbManager
    {
        private static readonly string DbName = "file-searcher-db.sqlite";

        private static IDbConnection Db => new SQLiteConnection($"Data Source={DbName}; Version=3;");

        /// <summary>
        /// Initializes the database file and creates necessary tables. 
        /// Call on app startup.
        /// </summary>
        public static void Initialize()
        {
            if (!File.Exists(DbName))
            {
                SQLiteConnection.CreateFile(DbName);
                var initScripts = new[]
                {
                    @"CREATE TABLE `SearchHistory` (
	                                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                                `Text`	TEXT NOT NULL,
	                                `Date`	BLOB NOT NULL
                                );",
                    @"CREATE TABLE `IndexedFolder` (
	                                `Id`	INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
	                                `Path`	TEXT NOT NULL,
	                                `Date`	BLOB NOT NULL
                                );"
                };

                using (var db = Db)
                {
                    foreach (var script in initScripts)
                    {
                        db.Execute(script);
                    }
                }
            }
        }

        public static List<SearchHistory> GetSearchHistory()
        {
            using (var db = Db)
            {
                return db.Query<SearchHistory>("Select * from SearchHistory").ToList();
            }
        }

        public static List<IndexedFolder> GetIndexedFolders()
        {
            using (var db = Db)
            {
                return db.Query<IndexedFolder>("Select * from IndexedFolder").ToList();
            }
        }

        public static void AddSearchHistory(SearchHistory searchHistory)
        {
            using (var db = Db)
            {
                db.Execute("Insert into SearchHistory values (@Text, @Date)", new { searchHistory.Text, DateTime.Now });
            }
        }

        public static void AddIndexedFolder(IndexedFolder indexedFolder)
        {
            using (var db = Db)
            {
                db.Execute("Insert into IndexedFolder values (@Path, @Date)", new { indexedFolder.Path, DateTime.Now });
            }
        }

    }
}
