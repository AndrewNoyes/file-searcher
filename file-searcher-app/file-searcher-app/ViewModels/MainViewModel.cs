using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using file_searcher_app.Db;
using file_searcher_app.Models;

namespace file_searcher_app.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Initialize();
        }

        private void Initialize()
        {
            DbManager.Initialize();
        }
    }
}
