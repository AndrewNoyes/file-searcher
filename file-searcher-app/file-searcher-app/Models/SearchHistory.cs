using System;

namespace file_searcher_app.Models
{
    public class SearchHistory 
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
