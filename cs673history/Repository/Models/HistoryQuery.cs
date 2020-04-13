namespace cs673history.Repository.Models
{
    public class HistoryQuery
    {
        public string User { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}
