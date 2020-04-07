using System;

namespace cs673history.Repository.Models
{

    public class HistoryItem
    {
        private Guid InternalId { get; } = Guid.NewGuid();
        public string Id => InternalId.ToString();
        public string Upc { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string User { get; set; }
    }
}
