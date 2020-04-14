namespace cs673history.Controllers.Models
{
    public class PageRequest
    {
        public int Skip { get; set; }
        public int Take { get; set; } = 10;
    }
}
