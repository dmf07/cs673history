using System;

namespace cs673history.Repository.Models
{

    public class HistoryItem
    {
        private Guid InternalId { get; set; } = Guid.NewGuid();

        public string Id
        {
            get => InternalId.ToString();
            set => InternalId = string.IsNullOrEmpty(value) ? Guid.NewGuid() : Guid.Parse(value);
        }

        public string Upc { get; set; }
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string User { get; set; }
    }
}
